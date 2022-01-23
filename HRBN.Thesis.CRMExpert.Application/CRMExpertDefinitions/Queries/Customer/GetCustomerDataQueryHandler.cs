using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;

public sealed class GetCustomerDataQueryHandler : IQueryHandler<GetCustomerDataQuery, List<CustomerDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerDataQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<CustomerDataDto>> HandleAsync(GetCustomerDataQuery query)
    {
        var results = await _unitOfWork.CustomersRepository.GetCustomerDataAsync();
        return results;
    }
}