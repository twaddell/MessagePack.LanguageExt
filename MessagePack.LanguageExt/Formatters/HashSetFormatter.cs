using MessagePack.Formatters;
using LanguageExt;
using static LanguageExt.Prelude;

namespace MessagePack.LanguageExt 
{
    public class HashSetFormatter<T> : CollectionFormatterBase<T, T[], HashSet<T>>
    {
        protected override void Add(T[] collection, int index, T value)
        {
            collection[index] = value;
        }

        protected override HashSet<T> Complete(T[] intermediateCollection)
        {
            return intermediateCollection.Fold(new HashSet<T>(), (s,x) => s.Add(x));
        }

        protected override T[] Create(int count)
        {
            return new T[count];
        }
    }
}