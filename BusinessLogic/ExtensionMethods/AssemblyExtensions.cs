using System.Reflection;

namespace LAF.BusinessLogic.ExtensionMethods
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly)
        {
            return assembly.GetTypesAssignableFrom(typeof(T));
        }

        public static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
        {
            List<Type> ret = [];
            foreach (var type in assembly.DefinedTypes)
            {
                if (compareType.IsAssignableFrom(type) && compareType != type)
                {
                    ret.Add(type);
                }
            }
            return ret;
        }
    }
}
