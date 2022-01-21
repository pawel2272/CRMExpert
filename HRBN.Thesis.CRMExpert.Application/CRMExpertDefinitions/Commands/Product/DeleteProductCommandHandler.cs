using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product
{
    public sealed class DeleteProductCommandHandler : CommandHandlerBase<DeleteProductCommand>
    {
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(DeleteProductCommand command)
        {
            var entity = await _unitOfWork.ProductsRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Product does not exist.");
            }

            await _unitOfWork.ProductsRepository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}