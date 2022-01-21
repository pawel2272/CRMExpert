using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    public sealed class LoginCommandHandler : CommandHandlerBase<LoginCommand>
    {
        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(LoginCommand command)
        {
            var validationResult = await new LoginCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var token = await _unitOfWork.UsersRepository.LoginAsync(command.Username, command.Password,
                command.RememberMe);
            if (string.IsNullOrEmpty(token))
            {
                return Result.Fail("Invalid username or password.");
            }

            return new Result(true, token, Enumerable.Empty<Result.Error>());
        }
    }
}