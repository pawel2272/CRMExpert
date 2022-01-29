using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Order
{
    public sealed class SearchOrdersQueryHandler : IQueryHandler<SearchOrdersQuery, PageResult<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<OrderDto>> HandleAsync(SearchOrdersQuery query)
        {
            var validationResult = await new SearchOrdersQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.OrdersRepository.SearchAsync(
                query.ContactId,
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new PageResult<OrderDto>(_mapper.Map<List<OrderDto>>(result.Items), result.TotalItemsCount,
                query.PageSize, query.PageNumber, query.SearchPhrase, query.SortDirection, query.OrderBy);
        }
    }
}