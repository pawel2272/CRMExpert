using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Order
{
    public sealed class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
