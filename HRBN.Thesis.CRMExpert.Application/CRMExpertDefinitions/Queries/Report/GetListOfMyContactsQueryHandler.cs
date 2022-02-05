using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Report;

public class GetListOfMyContactsQueryHandler : IQueryHandler<GetListOfMyContactsQuery, List<MyContactsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetListOfMyContactsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MyContactsDto>> HandleAsync(GetListOfMyContactsQuery query)
    {
        var entities = await _unitOfWork.ContactsRepository.GetContactByUserAsync(query.UserId);

        var mappedEntities = _mapper.Map<List<MyContactsDto>>(entities);

        return mappedEntities;
    }
}