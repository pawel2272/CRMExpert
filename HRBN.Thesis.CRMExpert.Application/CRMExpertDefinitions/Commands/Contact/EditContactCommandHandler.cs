using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact
{
    public sealed class EditContactCommandHandler : CommandHandlerBase<EditContactCommand>
    {
        public EditContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditContactCommand command)
        {
            var validationResult = await new EditContactCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var contact = await _unitOfWork.ContactsRepository.GetAsync(command.Id);
            if (contact == null)
            {
                return Result.Fail("Contact does not exist.");
            }

            _mapper.Map(command, contact);

            contact.ModDate = DateTime.Now;

            await _unitOfWork.ContactsRepository.UpdateAsync(contact);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
