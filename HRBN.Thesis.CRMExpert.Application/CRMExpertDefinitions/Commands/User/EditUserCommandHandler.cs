using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    public sealed class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditUserCommand command)
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

            _mapper.Map(command, user);

            await _unitOfWork.UsersRepository.UpdateAsync(user);
            user.ModDate = DateTime.Now;
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
