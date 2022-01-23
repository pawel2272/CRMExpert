using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;

public sealed class GetContactDataQueryHandler : IQueryHandler<GetContactDataQuery, List<ContactDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContactDataQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ContactDataDto>> HandleAsync(GetContactDataQuery query)
    {
        var result = await _unitOfWork.ContactsRepository.GetContactDataAsync();

        return result;
    }
}