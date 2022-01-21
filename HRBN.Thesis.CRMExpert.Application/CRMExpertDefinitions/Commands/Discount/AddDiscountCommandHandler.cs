using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    public sealed class AddDiscountCommandHandler : CommandHandlerBase<AddDiscountCommand>
    {
        public AddDiscountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddDiscountCommand command)
        {
            var validationResult = await new AddDiscountCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = _mapper.Map<Domain.Core.Entities.Discount>(command);
            entity.Id = Guid.NewGuid();

            entity.CreDate = DateTime.Now;
            entity.ModDate = DateTime.Now;

            await _unitOfWork.DiscountsRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}