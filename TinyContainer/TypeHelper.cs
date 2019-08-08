using System;
using System.Linq;
using System.Reflection;

namespace TinyContainer
{
    internal static class TypeHelper
    {
        public static ConstructorInfo GetConstructor(Type type)
        {
            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
        }

        public static object CreateInstance(TinyContainer container, ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            object[] args = parameters.Select(p => container.Resolve(p.ParameterType)).ToArray();

            return constructor.Invoke(args);
        }
    }
}