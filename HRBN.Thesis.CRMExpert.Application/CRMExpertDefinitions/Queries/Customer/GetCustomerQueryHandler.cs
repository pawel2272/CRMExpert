using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer
{
    public sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> HandleAsync(GetCustomerQuery query)
        {
            var customer = await _unitOfWork.CustomersRepository.GetAsync(query.Id);

            if (customer == null)
            {
                throw new NullReferenceException("Customer does not exist!");
            }

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
