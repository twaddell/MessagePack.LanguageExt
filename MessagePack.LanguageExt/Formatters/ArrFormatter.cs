using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt 
{
    public class ArrFormatter<T> : CollectionFormatterBase<T, T[], Arr<T>>
    {
        protected override void Add(T[] collection, int index, T value, MessagePackSerializerOptions options)
        {
            collection[index] = value;
        }

        protected override Arr<T> Complete(T[] intermediateCollection)
        {
            return new Arr<T>(intermediateCollection);
        }

        protected override T[] Create(int count, MessagePackSerializerOptions options)
        {
            return new T[count];
        }
    }
}