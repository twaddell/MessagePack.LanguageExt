using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class StckTests
    {
        protected Stck<int> CreateDataType()
            => new Stck<int>(Prelude.Range(1, 100));

        [TestMethod]
        public void ItShouldSerializeAndDeserializeUsingContractlessResolver()
        {
            var a = CreateDataType();
            var actual = TestHelpers.Convert(a);
            Assert.AreEqual(a.ToSeq(), actual.ToSeq());
        }

        [TestMethod]
        public void ItShouldSerializeAndDeserializeUsingTypelesssResolver()
        {
            var a = CreateDataType();
            var actual = TestHelpers.TypelessConvert(a);
            Assert.AreEqual(a.ToSeq(), actual.ToSeq());
        }
    }
}