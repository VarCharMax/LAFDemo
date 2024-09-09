namespace LAF
{
    namespace BusinessLogic.ExtensionMethods
    {
        public static class InputExtensions
        {
            /*
             * Utility method to prevent invalid values being assigned to data types.
             * */
            public static T LimitToRange<T>(
                this T value, T inclusiveMinimum, T inclusiveMaximum) where T : IComparable<T>
            {
                if (value.CompareTo(inclusiveMinimum) < 0)
                    return inclusiveMinimum;
                if (value.CompareTo(inclusiveMaximum) > 0)
                    return inclusiveMaximum;

                return value;
            }

            public static Dictionary<string, string> CSToDict(this string value)
            {
                return value.Split(';')
                    .Select(part => part.Split('='))
                    .Where(part => part.Length == 2)
                    .ToDictionary(sp => sp[0], sp => sp[1]);
            }

            public static string DictToString(this Dictionary<string, string> value)
            {
                return string.Join(";", value.Select(x => x.Key + "=" + x.Value).ToArray());
            }
        }
    }
}
