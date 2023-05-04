namespace StripeCreator.DAL.Tests.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> AddRangeFromCollections<T>(this IEnumerable<T> source, params T[][] collections)
        {
            var list = new List<T>(source);
            foreach (var item in collections)
            {
                list.AddRange(item);
            }
            return list;
        }
    }
}
