using System;
using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Report;

public class GetListOfMyContactsQuery : IQuery<List<MyContactsDto>>
{
    public GetListOfMyContactsQuery(Guid userId)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; }
}