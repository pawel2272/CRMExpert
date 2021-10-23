namespace HRBN.Thesis.CRMExpert.Application.Core.Query
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}