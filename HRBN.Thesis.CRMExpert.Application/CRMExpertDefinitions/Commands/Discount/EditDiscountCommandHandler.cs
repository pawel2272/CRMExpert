using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    public sealed class EditDiscountCommandHandler : CommandHandlerBase<EditDiscountCommand>
    {
        public EditDiscountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditDiscountCommand command)
        {
            var validationResult = await new EditDiscountCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = await _unitOfWork.DiscountsRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Discount does not exist.");
            }

            _mapper.Map(command, entity);

            entity.ModDate = DateTime.Now;

            await _unitOfWork.DiscountsRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
