using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;

public sealed class GetProductDataQueryHandler : IQueryHandler<GetProductDataQuery, List<ProductDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductDataQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductDataDto>> HandleAsync(GetProductDataQuery query)
    {
        var results = await _unitOfWork.ProductsRepository.GetProductDataAsync();
        return results;
    }
}