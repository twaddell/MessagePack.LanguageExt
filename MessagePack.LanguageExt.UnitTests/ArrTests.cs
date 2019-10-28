using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class ArrTests : BaseTests<Arr<int>>
    {
        protected override Arr<int> CreateDataType()
            => Prelude.Range(1, 100).ToArr();
    }
}