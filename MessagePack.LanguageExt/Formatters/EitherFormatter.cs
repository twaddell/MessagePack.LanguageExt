using MessagePack.Formatters;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace MessagePack.LanguageExt 
{
    public class EitherFormatter<TLeft, TRight> : CollectionFormatterBase<EitherData<TLeft, TRight>, EitherData<TLeft, TRight>[], Either<TLeft, TRight>>
    {
        protected override void Add(EitherData<TLeft, TRight>[] collection, int index, EitherData<TLeft, TRight> value)
        {
            collection[index] = value;
        }

        protected override Either<TLeft, TRight> Complete(EitherData<TLeft, TRight>[] intermediateCollection)
        {
            return new Either<TLeft, TRight>(intermediateCollection);
        }

        protected override EitherData<TLeft, TRight>[] Create(int count)
        {
            return new EitherData<TLeft, TRight>[count];
        }
    }
}