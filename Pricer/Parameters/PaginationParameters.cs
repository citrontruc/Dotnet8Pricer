/*
Method for pagination verification.
*/

namespace Pagination;

public class PaginationParameters
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }

    /// <summary>
    /// A method to check that the pages asked by the user follow pagination rules.
    /// </summary>
    /// /// <param name="paginationParameters">Parameters to respect for pagination</param>
    /// <param name="pageSize">Number of records asked by the user</param>
    /// <param name="pageNumber">Initial page asked by the user</param>
    /// <returns>(correctPageSize, correctPageNumber): corrected parameters following our constraints</returns>
    public static (int, int) CorrectPaginationParameters(
        PaginationParameters paginationParameters,
        int? pageSize,
        int? pageNumber
    )
    {
        int interPageSize = pageSize ?? paginationParameters.PageSize;
        int interPageNumber = pageNumber ?? paginationParameters.PageNumber;
        int correctPageSize =
            (paginationParameters.PageSize >= interPageSize)
                ? interPageSize
                : paginationParameters.PageSize;
        int correctPageNumber =
            (
                paginationParameters.PageNumber * paginationParameters.PageSize
                >= interPageNumber * correctPageSize
            )
                ? interPageNumber
                : paginationParameters.PageNumber * paginationParameters.PageSize - correctPageSize;
        return (correctPageSize, correctPageNumber);
    }
}
