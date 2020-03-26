using System;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
using LanguageExt;

namespace MessagePack.LanguageExt
{
    public class LanguageExtResolver : IFormatterResolver
    {
        public static readonly LanguageExtResolver Instance = new LanguageExtResolver();

        private LanguageExtResolver()
        {
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                Formatter = (IMessagePackFormatter<T>)LanguageExtGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }

    internal static class LanguageExtGetFormatterHelper
    {
        private static readonly Dictionary<Type, Type> FormatterMap = new Dictionary<Type, Type>()
        {
            { typeof(Arr<>), typeof(ArrFormatter<>) },
            { typeof(Lst<>), typeof(LstFormatter<>) },
            { typeof(Que<>), typeof(QueFormatter<>) },
            { typeof(Stck<>), typeof(StckFormatter<>) },
            { typeof(Seq<>), typeof(SeqFormatter<>) },
            { typeof(Set<>), typeof(SetFormatter<>) },
            { typeof(global::LanguageExt.HashSet<>), typeof(HashSetFormatter<>) },
            { typeof(Map<,>), typeof(MapFormatter<,>) },
            { typeof(HashMap<,>), typeof(HashMapFormatter<,>) },
            { typeof(Either<,>), typeof(EitherFormatter<,>) },
            { typeof(Option<>), typeof(OptionFormatter<>) },
            { typeof(NewType<,>), typeof(NewTypeFormatter<,>) },
        };

        internal static object GetFormatter(Type t)
        {
            TypeInfo ti = t.GetTypeInfo();

            if (ti.IsGenericType)
            {
                Type genericType = ti.GetGenericTypeDefinition();
                TypeInfo genericTypeInfo = genericType.GetTypeInfo();
                var isNullable = genericTypeInfo.IsNullable();
                Type nullableElementType = isNullable ? ti.GenericTypeArguments[0] : null;

                Type formatterType;
                if (FormatterMap.TryGetValue(genericType, out formatterType))
                {
                    return CreateInstance(formatterType, ti.GenericTypeArguments);
                }
            }
            else if (ti.BaseType?.IsGenericType ?? false)
            {
                Type genericType = ti.BaseType.GetGenericTypeDefinition();
                if (genericType == typeof(NewType<,>))
                {
                    return CreateInstance(typeof(NewTypeFormatter<,>), ti.BaseType.GenericTypeArguments);
                }
            }

            return null;
        }

        private static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
        {
            return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
        }
    }

    internal static class ReflectionExtensions
    {
        public static bool IsNullable(this System.Reflection.TypeInfo type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
        }
    }
}
