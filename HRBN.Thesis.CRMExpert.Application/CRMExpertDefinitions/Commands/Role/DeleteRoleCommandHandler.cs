﻿using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role
{
    public sealed class DeleteRoleCommandHandler : CommandHandlerBase<DeleteRoleCommand>
    {
        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteRoleCommand command)
        {
            var role = await _unitOfWork.RolesRepository.GetAsync(command.Id);
            if (role == null)
            {
                return Result.Fail("Role does not exist.");
            }

            await _unitOfWork.RolesRepository.DeleteAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
