using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role
{
    public class GetRoleQuery : IQuery<RoleDto>
    {
        public GetRoleQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
