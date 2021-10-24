using System.Threading.Tasks;

namespace HRBN.Thesis.CRMExpert.Application.Core.Query
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}