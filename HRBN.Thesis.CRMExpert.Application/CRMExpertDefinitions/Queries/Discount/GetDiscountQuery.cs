using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount
{
    public sealed class GetDiscountQuery : IQuery<DiscountDto>
    {
        public GetDiscountQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
