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
            var standardResolver = CompositeResolver.Create(
                BuiltinResolver.Instance,
                LanguageExtResolver.Instance,
                ContractlessStandardResolver.Instance);

            MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
                                                                               .WithResolver(standardResolver);

            var typelessResolver = CompositeResolver.Create(
                LanguageExtResolver.Instance,
                TypelessContractlessStandardResolver.Instance
            );

            MessagePackSerializer.Typeless.DefaultOptions = TypelessContractlessStandardResolver.Options
                                                                                                .WithResolver(typelessResolver);

            initialized = true;
        }

        [TestMethod]
        public void Initialize()
        {
            Assert.IsTrue(initialized);
        }
    }
}
