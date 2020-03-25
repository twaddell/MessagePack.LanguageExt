using System.Runtime.Serialization;
using LanguageExt;
using MessagePack.LanguageExt.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessagePack.LanguageExt.UnitTests
{
    [TestClass]
    public class NewDoubleTypeTests : BaseTests<CustomDoubleType>
    {
        protected override CustomDoubleType CreateDataType()
            => 10.50;

        [TestMethod]
        public void ItShouldConvertToJson()
        {
            string expected = "10.5";
            var a = CreateDataType();
            var data = MessagePackSerializer.Serialize(a);
            var actual = MessagePackSerializer.ConvertToJson(data);
            Assert.AreEqual(expected, actual);
        }
        
    }

    [MessagePackFormatter(typeof(NewTypeFormatter<CustomDoubleType, double>))]
    public class CustomDoubleType : NewType<CustomDoubleType, double> {
        public CustomDoubleType(double value) : base(value) { }
        public CustomDoubleType(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        public static implicit operator CustomDoubleType(double value)
            => new CustomDoubleType(value);

        public static implicit operator double(CustomDoubleType value)
            => value.Value;
    }
}