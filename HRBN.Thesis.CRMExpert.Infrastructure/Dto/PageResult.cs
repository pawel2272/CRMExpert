using System;
using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Dto
{
    public class PageResult<T> : IPageResult<T>
    {
        public List<T> Items { get; }
        public int TotalPages { get; }
        public int ItemsFrom { get; }
        public int ItemsTo { get; }
        public int TotalItemsCount { get; }
        public int PageNumber { get; }
        public string SearchPhrase { get; }
        public int PageSize { get; }
        public SortDirection SortDirection { get; }
        public string OrderBy { get; }
        public Guid ContactId { get; set; }

        public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber, string searchPhrase,
            SortDirection sortDirection, string orderBy)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            PageNumber = pageNumber;
            SearchPhrase = searchPhrase;
            PageSize = pageSize;
            SortDirection = sortDirection;
            OrderBy = orderBy;
        }
    }
}