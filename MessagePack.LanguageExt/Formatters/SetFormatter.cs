using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt 
{
    public class SetFormatter<T> : CollectionFormatterBase<T, T[], Set<T>>
    {
        protected override void Add(T[] collection, int index, T value, MessagePackSerializerOptions options)
        {
            collection[index] = value;
        }

        protected override Set<T> Complete(T[] intermediateCollection)
        {
            return new Set<T>(intermediateCollection);
        }

        protected override T[] Create(int count, MessagePackSerializerOptions options)
        {
            return new T[count];
        }
    }
}