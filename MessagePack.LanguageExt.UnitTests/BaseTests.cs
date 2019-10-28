using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using MessagePack.LanguageExt;
using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public abstract class BaseTests<T>
    {

        protected abstract T CreateDataType();

        [TestMethod]
        public void ItShouldSerializeAndDeserializeUsingContractlessResolver()
        {
            var a = CreateDataType();
            var actual = TestHelpers.Convert(a);
            Assert.AreEqual(a, actual);
        }

        [TestMethod]
        public void ItShouldSerializeAndDeserializeUsingTypelesssResolver()
        {
            var a = CreateDataType();
            var actual = TestHelpers.TypelessConvert(a);
            Assert.AreEqual(a, actual);
        }
    }
}
