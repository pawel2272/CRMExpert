namespace HRBN.Thesis.CRMExpert.Application.Core.Mediator
{
    public interface IDependencyResolver
    {
        T ResolveOrDefault<T>() where T : class;
    }
}