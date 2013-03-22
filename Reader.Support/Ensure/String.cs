namespace Reader.StaticAnalysis
{
    using System;

    public static partial class Ensure
    {
        public static void IsNullOrWhitespace<T>(string s)
            where T: Exception, new()
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                throw new T();
            }
        }

        public static void IsNotNullOrWhitespace<T>(string s)
            where T : Exception, new()
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new T();
            }
        }
    }
}