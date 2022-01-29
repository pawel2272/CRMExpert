using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact
{
    public sealed class SearchContactsQueryHandler : IQueryHandler<SearchContactsQuery, PageResult<ContactDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchContactsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<ContactDto>> HandleAsync(SearchContactsQuery query)
        {
            var validationResult = await new SearchContactsQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.ContactsRepository.SearchAsync(
                //query.UserId,
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new PageResult<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items), result.TotalItemsCount,
                query.PageSize, query.PageNumber, query.SearchPhrase, query.SortDirection, query.OrderBy);
        }
    }
}