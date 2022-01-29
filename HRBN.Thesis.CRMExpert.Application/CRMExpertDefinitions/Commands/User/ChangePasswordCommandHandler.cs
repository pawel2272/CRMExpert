using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

public sealed class ChangePasswordCommandHandler : CommandHandlerBase<ChangePasswordCommand>
{
    public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<Result> HandleAsync(ChangePasswordCommand command)
    {
        var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
        if (user == null)
        {
            return Result.Fail("User does not exist.");
        }

        var validationResult =
            await new ChangePasswordCommandValidator(
                    await _unitOfWork.UsersRepository.IsPasswordValid(command.Id, command.OldPassword))
                .ValidateAsync(command);
        
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult);
        }

        user.Password = command.NewPassword;

        await _unitOfWork.UsersRepository.UpdateAsync(user);
        user.ModDate = DateTime.Now;
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }
}