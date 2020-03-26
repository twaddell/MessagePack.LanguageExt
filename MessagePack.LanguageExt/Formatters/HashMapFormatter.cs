using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt
{
    public class HashMapFormatter<TKey, TValue> : CollectionFormatterBase<(TKey, TValue), (TKey, TValue)[], HashMap<TKey, TValue>>
    {
        protected override void Add((TKey, TValue)[] collection, int index, (TKey, TValue) value, MessagePackSerializerOptions options)
        {
            collection[index] = value;
        }

        protected override HashMap<TKey, TValue> Complete((TKey, TValue)[] intermediateCollection)
        {
            return new HashMap<TKey, TValue>(intermediateCollection);
        }

        protected override (TKey, TValue)[] Create(int count, MessagePackSerializerOptions options)
        {
            return new (TKey, TValue)[count];
        }
    }
}