using System.Runtime.Serialization;
using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public class NewStringTypeTests : BaseTests<NewStringType>
    {
        protected override NewStringType CreateDataType()
            => "Hello World";

        [TestMethod]
        public void ItShouldConvertToJson()
        {
            string expected = "\"Hello World\"";
            var a = CreateDataType();
            var data = MessagePackSerializer.Serialize(a);
            var actual = MessagePackSerializer.ConvertToJson(data);
            Assert.AreEqual(expected, actual);
        }
        
    }

    public class NewStringType : NewType<NewStringType, string> {
        public NewStringType(string value) : base(value) { }
        public NewStringType(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        public static implicit operator NewStringType(string value)
            => new NewStringType(value);

        public static implicit operator string(NewStringType value)
            => value.Value;
    }
}