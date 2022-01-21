using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer
{
    public sealed class GetCustomerQuery : IQuery<CustomerDto>
    {
        public GetCustomerQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
