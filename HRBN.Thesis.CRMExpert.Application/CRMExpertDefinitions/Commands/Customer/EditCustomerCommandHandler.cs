using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer
{
    public sealed class EditCustomerCommandHandler : CommandHandlerBase<EditCustomerCommand>
    {
        public EditCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditCustomerCommand command)
        {
            var validationResult = await new EditCustomerCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = await _unitOfWork.CustomersRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Customer does not exist.");
            }

            _mapper.Map(command, entity);

            entity.ModDate = DateTime.Now;

            await _unitOfWork.CustomersRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
