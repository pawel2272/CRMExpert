using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product
{
    public sealed class EditProductCommandHandler : CommandHandlerBase<EditProductCommand>
    {
        public EditProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(EditProductCommand command)
        {
            var validationResult = await new EditProductCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = await _unitOfWork.ProductsRepository.GetAsync(command.Id);
            if (entity == null)
            {
                return Result.Fail("Product does not exist.");
            }

            _mapper.Map(command, entity);

            entity.ModDate = DateTime.Now;

            await _unitOfWork.ProductsRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
