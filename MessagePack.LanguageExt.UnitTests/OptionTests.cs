using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class OptionTests : BaseTests<Option<int>>
    {
        protected override Option<int> CreateDataType()
            => 1;
        
        [TestMethod]
        public void ItShouldSerializeAndDeserializeNoneUsingContractlessResolver()
        {
            var a = Option<int>.None;
            var actual = TestHelpers.Convert(a);
            Assert.AreEqual(a, actual);
        }

        [TestMethod]
        public void ItShouldSerializeAndDeserializeNoneUsingTypelesssResolver()
        {
            var a = Option<int>.None;
            var actual = TestHelpers.TypelessConvert(a);
            Assert.AreEqual(a, actual);
        }

    }
}