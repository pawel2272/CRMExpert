using System.Threading.Tasks;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    /// <summary>
    /// Interface command handler CQRS.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command);
    }
}