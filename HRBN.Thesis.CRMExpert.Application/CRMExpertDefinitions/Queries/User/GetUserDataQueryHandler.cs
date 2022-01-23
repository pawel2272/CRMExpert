using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;

public sealed class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, List<UserDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserDataQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDataDto>> HandleAsync(GetUserDataQuery query)
    {
        var results = await _unitOfWork.UsersRepository.GetUserDataAsync();
        return results;
    }
}