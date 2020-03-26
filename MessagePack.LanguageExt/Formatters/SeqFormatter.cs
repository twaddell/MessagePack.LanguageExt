using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt
{
    public class SeqFormatter<T> : CollectionFormatterBase<T, T[], Seq<T>>
    {
        protected override void Add(T[] collection, int index, T value, MessagePackSerializerOptions options)
        {
            collection[index] = value;
        }

        protected override Seq<T> Complete(T[] intermediateCollection)
        {
            return intermediateCollection.ToSeq();
        }

        protected override T[] Create(int count, MessagePackSerializerOptions options)
        {
            return new T[count];
        }
    }
}