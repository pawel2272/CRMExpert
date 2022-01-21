using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    public sealed class DeletePermissionCommandHandler : CommandHandlerBase<DeletePermissionCommand>
    {
        public DeletePermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeletePermissionCommand command)
        {
            var entity = await _unitOfWork.PermissionsRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Permission does not exist.");
            }

            await _unitOfWork.PermissionsRepository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
