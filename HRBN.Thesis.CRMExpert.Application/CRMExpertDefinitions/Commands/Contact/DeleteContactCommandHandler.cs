using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact
{
    public sealed class DeleteContactCommandHandler : CommandHandlerBase<DeleteContactCommand>
    {
        public DeleteContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteContactCommand command)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(command.Id);
            if (contact == null)
            {
                return Result.Fail("Contact does not exist.");
            }

            await _unitOfWork.ContactsRepository.DeleteAsync(contact);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}