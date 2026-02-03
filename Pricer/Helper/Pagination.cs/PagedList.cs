/*
A child of the list method to handle data with pagination.
*/

using Microsoft.EntityFrameworkCore;

namespace ListPagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(IEnumerable<T> items, int pageSize, int pageNumber, int count)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        #region Conversion methods
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageSize, int pageNumber)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, pageSize, pageNumber, count);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(
            IQueryable<T> source,
            int pageSize,
            int pageNumber
        )
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, pageSize, pageNumber, count);
        }
        #endregion
    }
}
