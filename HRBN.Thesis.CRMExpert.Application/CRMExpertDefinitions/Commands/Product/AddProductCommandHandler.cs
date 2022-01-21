using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core;
using HRBN.Thesis.CRMExpert.Application.Core.Command;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product
{
    public sealed class AddProductCommandHandler : CommandHandlerBase<AddProductCommand>
    {
        public AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<Result> HandleAsync(AddProductCommand command)
        {
            var validationResult = await new AddProductCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var entity = _mapper.Map<Domain.Core.Entities.Product>(command);
            entity.Id = Guid.NewGuid();

            entity.CreDate = DateTime.Now;
            entity.ModDate = DateTime.Now;

            await _unitOfWork.ProductsRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}