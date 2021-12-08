using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact
{
    public sealed class GetContactQuery : IQuery<ContactDto>
    {
        public GetContactQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
