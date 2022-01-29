using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;

public sealed class ChangeAddressCommandHandler : CommandHandlerBase<ChangeAddressCommand>
{
    public ChangeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<Result> HandleAsync(ChangeAddressCommand command)
    {
        var validationResult = await new ChangeAddressCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult);
        }

        var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
        if (user == null)
        {
            return Result.Fail("User does not exist.");
        }

        user.Phone = command.Phone;
        user.Email = command.Email;
        user.Street = command.Street;
        user.PostalCode = command.PostalCode;
        user.City = command.City;

        await _unitOfWork.UsersRepository.UpdateAsync(user);
        user.ModDate = DateTime.Now;
        await _unitOfWork.CommitAsync();

        return Result.Ok();
    }
}