using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class EitherTests : BaseTests<Either<string, int>>
    {
        protected override Either<string, int> CreateDataType()
            => 1;

        [TestMethod]
        public void ItShouldSerializeAndDeserializeLeftValueContractlessResolver()
        {
            var a = Prelude.Left<string,int>("test value");
            var actual = TestHelpers.Convert(a);
            Assert.AreEqual(a, actual);
        }

        [TestMethod]
        public void ItShouldSerializeAndDeserializeLeftValueUsingTypelesssResolver()
        {
            var a = Prelude.Left<string, int>("test value");
            var actual = TestHelpers.TypelessConvert(a);
            Assert.AreEqual(a, actual);
        }
    }
}