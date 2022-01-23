using System;
using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;

public sealed class GetContactDataByUserQuery : IQuery<List<ContactDataDto>>
{
    public GetContactDataByUserQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}