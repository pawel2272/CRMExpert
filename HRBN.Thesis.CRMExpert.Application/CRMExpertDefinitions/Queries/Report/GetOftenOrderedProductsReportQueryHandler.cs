using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Report;

public class GetOftenOrderedProductsReportQueryHandler : IQueryHandler<GetOftenOrderedProductsReportQuery, List<OftenOrderedProductsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOftenOrderedProductsReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OftenOrderedProductsDto>> HandleAsync(GetOftenOrderedProductsReportQuery query)
    {
        var results = await _unitOfWork.ProductsRepository.GetOftenOrderedProducts();

        var mappedResults = _mapper.Map<List<OftenOrderedProductsDto>>(results);

        return mappedResults;
    }
}