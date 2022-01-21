using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Permission
{
    public sealed class GetPermissionQueryHandler : IQueryHandler<GetPermissionQuery, PermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissionDto> HandleAsync(GetPermissionQuery query)
        {
            var contact = await _unitOfWork.ContactsRepository.GetAsync(query.Id);

            if (contact == null)
            {
                throw new NullReferenceException("Permission does not exist!");
            }

            return _mapper.Map<PermissionDto>(contact);
        }
    }
}
