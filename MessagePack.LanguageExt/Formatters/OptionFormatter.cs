using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt 
{
    public class OptionFormatter<T> : CollectionFormatterBase<T, T[], Option<T>>
    {
        protected override void Add(T[] collection, int index, T value)
        {
            collection[index] = value;
        }

        protected override Option<T> Complete(T[] intermediateCollection)
        {
            return new Option<T>(intermediateCollection);
        }

        protected override T[] Create(int count)
        {
            return new T[count];
        }
    }
}