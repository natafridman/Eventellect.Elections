namespace Elections.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Clear<T>(this IEnumerable<T> enumerable) => enumerable.Take(0);

        public static T? Second<T>(this IEnumerable<T> enumerable) where T : class => (enumerable.Count() > 1) ? enumerable.ElementAt(1) : null;

        public static IEnumerable<T> RemoveFirst<T>(this IEnumerable<T> enumerable) => enumerable.Skip(1);
    }
}