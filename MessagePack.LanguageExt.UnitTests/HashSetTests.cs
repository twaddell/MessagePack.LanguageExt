using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class HashSetTests : BaseTests<global::LanguageExt.HashSet<int>>
    {
        protected override global::LanguageExt.HashSet<int> CreateDataType()
            => Prelude.Range(1, 100).Fold(Prelude.HashSet<int>(), (s, x) => s.Add(x));
    }
}