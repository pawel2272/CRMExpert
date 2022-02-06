using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

public interface IDiscountsRepository : IRepository<Discount>
{
    Task<Discount> GetProductDiscountAsync(Guid productId, Guid customerId);
}