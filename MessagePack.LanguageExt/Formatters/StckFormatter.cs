using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt 
{
    public class StckFormatter<T> : CollectionFormatterBase<T, T[], Stck<T>>
    {
        protected override void Add(T[] collection, int index, T value, MessagePackSerializerOptions options)
        {
            collection[collection.Length - index - 1] = value;
        }

        protected override Stck<T> Complete(T[] intermediateCollection)
        {
            return new Stck<T>(intermediateCollection);
        }

        protected override T[] Create(int count, MessagePackSerializerOptions options)
        {
            return new T[count];
        }
    }
}