using System.Runtime.Serialization;
using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public class NewIntTypeTests : BaseTests<CustomIntType>
    {
        protected override CustomIntType CreateDataType()
            => 1000;

        [TestMethod]
        public void ItShouldConvertToJson()
        {
            string expected = "1000";
            var a = CreateDataType();
            var data = MessagePackSerializer.Serialize(a);
            var actual = MessagePackSerializer.ConvertToJson(data);
            Assert.AreEqual(expected, actual);
        }
        
    }

    [MessagePackFormatter(typeof(NewTypeFormatter<CustomIntType, int>))]
    public class CustomIntType : NewType<CustomIntType, int> {
        public CustomIntType(int value) : base(value) { }
        public CustomIntType(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        public static implicit operator CustomIntType(int value)
            => new CustomIntType(value);

        public static implicit operator int(CustomIntType value)
            => value.Value;
    }
}