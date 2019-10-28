using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt
{
    public class MapFormatter<TKey, TValue> : CollectionFormatterBase<(TKey, TValue), (TKey, TValue)[], Map<TKey, TValue>>
    {
        protected override void Add((TKey, TValue)[] collection, int index, (TKey, TValue) value)
        {
            collection[index] = value;
        }

        protected override Map<TKey, TValue> Complete((TKey, TValue)[] intermediateCollection)
        {
            return new Map<TKey, TValue>(intermediateCollection);
        }

        protected override (TKey, TValue)[] Create(int count)
        {
            return new (TKey, TValue)[count];
        }
    }
}