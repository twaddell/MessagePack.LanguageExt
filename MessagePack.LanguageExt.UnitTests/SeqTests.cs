using LanguageExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests {
    [TestClass]
    public class SeqTests : BaseTests<Seq<int>>
    {
        protected override Seq<int> CreateDataType()
            => Prelude.Range(1, 100).ToSeq();
    }
}