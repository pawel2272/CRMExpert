using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role
{
    public sealed class AddRoleCommandHandler : CommandHandlerBase<AddRoleCommand>
    {
        public AddRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddRoleCommand command)
        {
            var validationResult = await new AddRoleCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var role = _mapper.Map<Domain.Core.Entities.Role>(command);
            await _unitOfWork.RolesRepository.AddAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
