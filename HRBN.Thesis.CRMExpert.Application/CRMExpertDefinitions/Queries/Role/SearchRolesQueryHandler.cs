using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role
{
    public class SearchRolesQueryHandler : IQueryHandler<SearchRolesQuery, PageResult<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<RoleDto>> HandleAsync(SearchRolesQuery query)
        {
            var validationResult = await new SearchRolesQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.RolesRepository.SearchAsync(
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new PageResult<RoleDto>(_mapper.Map<List<RoleDto>>(result.Items), result.TotalItemsCount,
                query.PageSize, query.PageNumber, query.SearchPhrase, query.SortDirection, query.OrderBy);
        }
    }
}