using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount
{
    public sealed class SearchPermissionQueryHandler : IQueryHandler<SearchDiscountsQuery, PageResult<DiscountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchPermissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<DiscountDto>> HandleAsync(SearchDiscountsQuery query)
        {
            var validationResult = await new SearchDiscountsQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.DiscountsRepository.SearchAsync(
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new PageResult<DiscountDto>(_mapper.Map<List<DiscountDto>>(result.Items), result.TotalItemsCount,
                query.PageSize, query.PageNumber, query.SearchPhrase, query.SortDirection, query.OrderBy);
        }
    }
}