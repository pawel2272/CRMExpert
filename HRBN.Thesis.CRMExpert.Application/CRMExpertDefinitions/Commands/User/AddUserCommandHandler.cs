using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User
{
    public sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddUserCommand command)
        {
            var validationResult = await new AddUserCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var user = _mapper.Map<Domain.Core.Entities.User>(command);
            user.Id = Guid.NewGuid();
            user.CreDate = DateTime.Now;
            user.ModDate = DateTime.Now;
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
