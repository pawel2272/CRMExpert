using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(LogoutCommand command)
        {
            await _unitOfWork.UsersRepository.LogoutAsync();
            await _unitOfWork.TokenRepository.DeactivateCurrentAsync();
            return Result.Ok();
        }
    }
}
