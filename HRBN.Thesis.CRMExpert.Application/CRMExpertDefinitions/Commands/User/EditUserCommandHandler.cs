using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    public sealed class EditUserCommandHandler : CommandHandlerBase<EditUserCommand>
    {
        public EditUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditUserCommand command)
        {
            var validationResult = await new EditUserCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
            if (user == null)
            {
                return Result.Fail("User does not exist.");
            }

            if (string.IsNullOrEmpty(command.Password))
            {
                command.Password = user.Password;
            }

            if (command.Password.Equals(user.Password))
            {
                _mapper.Map(command, user);
                await _unitOfWork.UsersRepository.UpdateAsync(user);
            }
            else
            {
                _mapper.Map(command, user);
                await _unitOfWork.UsersRepository.UpdateAndHashAsync(user);
            }
            
            user.ModDate = DateTime.Now;
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
