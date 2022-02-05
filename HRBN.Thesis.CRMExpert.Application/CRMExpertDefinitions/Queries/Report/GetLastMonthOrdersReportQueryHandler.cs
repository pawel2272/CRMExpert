using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Report;

public class GetLastMonthOrdersReportQueryHandler : IQueryHandler<GetLastMonthOrdersReportQuery, List<LastOrdersDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLastMonthOrdersReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<LastOrdersDto>> HandleAsync(GetLastMonthOrdersReportQuery query)
    {
        var result = await _unitOfWork.OrdersRepository.GetLastOrders(30);

        return _mapper.Map<List<LastOrdersDto>>(result);
    }
}