using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role
{
    public sealed class EditRoleCommandHandler : ICommandHandler<EditRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditRoleCommand command)
        {
            var validationResult = await new EditRoleCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var role = await _unitOfWork.RolesRepository.GetAsync(command.Id);
            if (role == null)
            {
                return Result.Fail("Role does not exist.");
            }

            _mapper.Map(command, role);

            await _unitOfWork.RolesRepository.UpdateAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
