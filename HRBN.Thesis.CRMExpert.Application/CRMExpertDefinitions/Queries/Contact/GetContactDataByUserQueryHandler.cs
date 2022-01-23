using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;

public sealed class GetContactDataByUserQueryHandler : IQueryHandler<GetContactDataByUserQuery, List<ContactDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContactDataByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ContactDataDto>> HandleAsync(GetContactDataByUserQuery query)
    {
        var user = await _unitOfWork.UsersRepository.GetAsync(query.Id);

        if (user == null)
        {
            throw new NullReferenceException("User does not exist!");
        }
        
        var result = await _unitOfWork.ContactsRepository.GetContactDataAsyncByUser(query.Id);

        return result;
    }
}