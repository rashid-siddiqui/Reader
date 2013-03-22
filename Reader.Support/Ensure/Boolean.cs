namespace Reader.StaticAnalysis
{
    using System;

    public static partial class Ensure
    {
        public static void IsTrue<T>(bool b)
            where T : Exception, new()
        {
            if (!b) throw new T();
        }

        public static void IsFalse<T>(bool b)
            where T : Exception, new()
        {
            if (b) throw new T();
        }
    }
}