using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount
{
    public sealed class DeleteDiscountCommandHandler : CommandHandlerBase<DeleteDiscountCommand>
    {
        public DeleteDiscountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteDiscountCommand command)
        {
            var entity = await _unitOfWork.DiscountsRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Discount does not exist.");
            }

            await _unitOfWork.DiscountsRepository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}