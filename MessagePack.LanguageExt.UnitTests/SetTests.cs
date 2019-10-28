using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class SetTests : BaseTests<Set<int>>
    {
        protected override Set<int> CreateDataType()
            => new Set<int>(Prelude.Range(1, 100));
    }
}