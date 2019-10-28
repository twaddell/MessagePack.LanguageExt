using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class HashMapTests : BaseTests<HashMap<int, string>>
    {
        protected override HashMap<int, string> CreateDataType()
            => Prelude.Range(1, 100).Fold(Prelude.HashMap<int, string>(), (s, x) => s.Add(x, x.ToString()));
    }
}