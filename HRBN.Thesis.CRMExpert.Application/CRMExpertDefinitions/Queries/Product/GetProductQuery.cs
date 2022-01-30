using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product
{
    public sealed class GetProductQuery : IQuery<ProductDto>
    {
        public GetProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
