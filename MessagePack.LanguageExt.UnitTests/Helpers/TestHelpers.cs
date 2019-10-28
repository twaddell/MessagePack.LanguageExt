using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.LanguageExt.UnitTests.Helpers
{
    public static class TestHelpers
    {
        public static T Convert<T>(T value)
        {
            var data = MessagePackSerializer.Serialize(value);
            Console.WriteLine(MessagePackSerializer.ToJson(data));
            return MessagePackSerializer.Deserialize<T>(data);
        }

        public static T TypelessConvert<T>(T value)
        {
            var data = MessagePackSerializer.Typeless.Serialize(value);
            Console.WriteLine(MessagePackSerializer.ToJson(data));
            return (T)MessagePackSerializer.Typeless.Deserialize(data);
        }
    }
}
