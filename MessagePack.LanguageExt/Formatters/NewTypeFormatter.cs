using System;
using System.Collections.Generic;
using System.Text;
using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt
{
    public class NewTypeFormatter<NEWTYPE, A> : IMessagePackFormatter<NEWTYPE> where NEWTYPE : NewType<NEWTYPE, A>
    {
        public void Serialize(ref MessagePackWriter writer, NEWTYPE value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            var formatter = options.Resolver.GetFormatterWithVerify<A>();
            formatter.Serialize(ref writer, value.Value, options);
        }

        public NEWTYPE Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return (NEWTYPE)Activator.CreateInstance(typeof(NEWTYPE), default(A));
            }


            var formatter = options.Resolver.GetFormatterWithVerify<A>();
            options.Security.DepthStep(ref reader);

            var data = formatter.Deserialize(ref reader, options);

            reader.Depth--;
            return (NEWTYPE)Activator.CreateInstance(typeof(NEWTYPE), data);
        }
    }
}