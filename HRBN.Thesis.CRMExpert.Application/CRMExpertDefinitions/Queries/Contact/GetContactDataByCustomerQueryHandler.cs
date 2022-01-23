using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;

public sealed class GetContactDataByCustomerQueryHandler : IQueryHandler<GetContactDataByCustomerQuery, List<ContactDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContactDataByCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ContactDataDto>> HandleAsync(GetContactDataByCustomerQuery query)
    {
        var customer = await _unitOfWork.CustomersRepository.GetAsync(query.Id);

        if (customer == null)
        {
            throw new NullReferenceException("Customer does not exist!");
        }
        
        var result = await _unitOfWork.ContactsRepository.GetContactDataAsyncByCustomer(query.Id);

        return result;
    }
}