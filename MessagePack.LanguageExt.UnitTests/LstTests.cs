using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class LstTests : BaseTests<Lst<int>>
    {
        protected override Lst<int> CreateDataType()
            => new Lst<int>(Prelude.Range(1, 100));
    }
}