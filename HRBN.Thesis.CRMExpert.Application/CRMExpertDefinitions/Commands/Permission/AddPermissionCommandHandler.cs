using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    public sealed class AddPermissionCommandHandler : CommandHandlerBase<AddPermissionCommand>
    {
        public AddPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddPermissionCommand command)
        {
            var validationResult = await new AddPermissionCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = _mapper.Map<Domain.Core.Entities.Permission>(command);
            entity.Id = Guid.NewGuid();
            entity.ModDate = DateTime.Now;
            entity.CreDate = DateTime.Now;
            await _unitOfWork.PermissionsRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
