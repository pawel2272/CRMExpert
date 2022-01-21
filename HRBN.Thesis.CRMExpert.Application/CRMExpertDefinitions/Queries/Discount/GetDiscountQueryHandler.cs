using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount
{
    public sealed class GetDiscountQueryHandler : IQueryHandler<GetDiscountQuery, DiscountDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDiscountQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DiscountDto> HandleAsync(GetDiscountQuery query)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(query.Id);

            if (contact == null)
            {
                throw new NullReferenceException("Discount does not exist!");
            }

            return _mapper.Map<DiscountDto>(contact);
        }
    }
}
