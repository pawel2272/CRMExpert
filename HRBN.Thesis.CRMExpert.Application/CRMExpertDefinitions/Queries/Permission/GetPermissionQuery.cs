using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Permission
{
    public sealed class GetPermissionQuery : IQuery<PermissionDto>
    {
        public GetPermissionQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
