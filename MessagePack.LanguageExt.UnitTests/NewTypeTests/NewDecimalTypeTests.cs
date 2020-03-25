using System.Runtime.Serialization;
using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public class NewDecimalTypeTests : BaseTests<CustomDecimalType>
    {
        protected override CustomDecimalType CreateDataType()
            => 10.50m;

        [TestMethod]
        public void ItShouldConvertToJson()
        {
            string expected = "\"10.50\"";
            var a = CreateDataType();
            var data = MessagePackSerializer.Serialize(a);
            var actual = MessagePackSerializer.ConvertToJson(data);
            Assert.AreEqual(expected, actual);
        }
        
    }

    [MessagePackFormatter(typeof(NewTypeFormatter<CustomDecimalType, decimal>))]
    public class CustomDecimalType : NewType<CustomDecimalType, decimal> {
        public CustomDecimalType(decimal value) : base(value) { }
        public CustomDecimalType(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        public static implicit operator CustomDecimalType(decimal value)
            => new CustomDecimalType(value);

        public static implicit operator decimal(CustomDecimalType value)
            => value.Value;
    }
}