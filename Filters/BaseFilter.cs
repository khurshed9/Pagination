﻿namespace DTOWithSRM.Filters;

public class BaseFilter
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public BaseFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public BaseFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0? 10 : pageSize;
    }
}