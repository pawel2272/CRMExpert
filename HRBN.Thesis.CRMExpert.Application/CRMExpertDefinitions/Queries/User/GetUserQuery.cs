using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User
{
    public sealed class GetUserQuery : IQuery<UserDto>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
