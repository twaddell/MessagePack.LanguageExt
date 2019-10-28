using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class MapTests : BaseTests<Map<int, string>>
    {
        protected override Map<int, string> CreateDataType()
            => Prelude.Range(1, 100).Fold(Prelude.Map<int, string>(), (s, x) => s.Add(x, x.ToString()));
    }
}