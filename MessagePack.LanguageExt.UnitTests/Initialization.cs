using System;
using System.Collections.Generic;
using System.Text;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public class Initialization
    {
        private static bool initialized;

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext testContext)
        {
            CompositeResolver.RegisterAndSetAsDefault(
                BuiltinResolver.Instance,
                LanguageExtResolver.Instance,
                ContractlessStandardResolver.Instance);

            MessagePackSerializer.Typeless.RegisterDefaultResolver(
                LanguageExtResolver.Instance,
                TypelessContractlessStandardResolver.Instance);

            initialized = true;
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.IsTrue(initialized);
        }
    }
}
