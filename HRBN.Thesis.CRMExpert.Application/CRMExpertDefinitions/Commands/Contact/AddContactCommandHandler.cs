using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact
{
    public sealed class AddContactCommandHandler : CommandHandlerBase<AddContactCommand>
    {
        public AddContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddContactCommand command)
        {
            var validationResult = await new AddContactCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var contact = _mapper.Map<Domain.Core.Entities.Contact>(command);
            contact.Id = Guid.NewGuid();

            contact.CreDate = DateTime.Now;
            contact.ModDate = DateTime.Now;

            await _unitOfWork.ContactsRepository.AddAsync(contact);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}