using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> HandleAsync(GetProductQuery query)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(query.Id);

            if (contact == null)
            {
                throw new NullReferenceException("Product does not exist!");
            }

            return _mapper.Map<ProductDto>(contact);
        }
    }
}
