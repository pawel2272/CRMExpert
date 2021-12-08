using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role
{
    public sealed class GetRoleQueryHandler : IQueryHandler<GetRoleQuery, RoleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> HandleAsync(GetRoleQuery query)
        {
            var role = await _unitOfWork.RolesRepository.GetAsync(query.Id);

            if (role == null)
            {
                throw new NullReferenceException("Role does not exist!");
            }

            return _mapper.Map<RoleDto>(role);
        }
    }
}
