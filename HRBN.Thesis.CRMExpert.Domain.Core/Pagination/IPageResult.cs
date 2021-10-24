using System.Collections.Generic;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Pagination
{
    public interface IPageResult<T>
    {
        List<T> Items { get; }
        int TotalPages { get; }
        int ItemsFrom { get; }
        int ItemsTo { get; }
        int TotalItemsCount { get; }
    }
}