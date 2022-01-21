using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer
{
    public sealed class DeleteCustomerCommandHandler : CommandHandlerBase<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteCustomerCommand command)
        {
            var entity = await _unitOfWork.CustomersRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Customer does not exist.");
            }

            await _unitOfWork.CustomersRepository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}