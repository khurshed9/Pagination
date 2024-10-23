using DTOWithSRM.Filters;

namespace DTO_Pagination_Filtering_Mapping;

public class PaginationResponses<T> : BaseFilter
{
    public int TotalPages { get; init; }

    public int TotalRecords { get; init; }

    public T Data { get; set; }

    private PaginationResponses(int pageNumber, int pageSize, int totalRecords, T data) : base(pageNumber, pageSize)
    {
        Data = data;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    public static PaginationResponses<T> Create(int pageNumber, int pageSize, int totalRecords, T data)
        => new PaginationResponses<T>(pageNumber, pageSize, totalRecords, data);
}