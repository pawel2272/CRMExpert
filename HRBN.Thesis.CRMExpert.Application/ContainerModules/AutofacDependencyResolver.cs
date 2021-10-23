using Autofac;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;

namespace HRBN.Thesis.CRMExpert.Application.ContainerModules
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public T ResolveOrDefault<T>() where T : class
        {
            return _lifetimeScope.ResolveOptional<T>();
        }
    }
}