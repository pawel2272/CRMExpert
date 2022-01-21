using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission
{
    public sealed class EditPermissionCommandHandler : CommandHandlerBase<EditPermissionCommand>
    {
        public EditPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditPermissionCommand command)
        {
            var validationResult = await new EditPermissionCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = await _unitOfWork.PermissionsRepository.GetAsync(command.Id);
            if (order == null)
            {
                return Result.Fail("Permission does not exist.");
            }

            _mapper.Map(command, order);

            order.ModDate = DateTime.Now;

            await _unitOfWork.PermissionsRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
