namespace TaskManagementSystem.Models
{
    public static class PagingUtils
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, CustomQueryParameters queryParameters)
        {
            if (queryParameters == null)
            {
                Console.WriteLine("Null query parameters");
                return en;
            }
            int page = queryParameters.Page - 1;
            return en.Skip(page * queryParameters.PageCount).Take(queryParameters.PageCount);
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> en, CustomQueryParameters queryParameters)
        {
            if (queryParameters == null)
            {
                Console.WriteLine("Null query parameters");
                return en;
            }
            int page = queryParameters.Page - 1;
            return en.Skip(page * queryParameters.PageCount).Take(queryParameters.PageCount);
        }
    }
}