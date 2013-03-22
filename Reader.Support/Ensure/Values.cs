namespace Reader.StaticAnalysis
{
    using System;

    public static partial class Ensure
    {
        public static void AreEqual<T, U>(U first, U second)
            where T: Exception, new()
            where U: IComparable
        {
            if (first.CompareTo(second) != 0)
            {
                throw new T();
            }
        }

        public static void IsNotNull<T>(object first)
            where T: Exception, new()
        {
            if (first == null)
            {
                throw new T();
            }
        }
    }
}