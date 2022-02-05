using System.Collections.Generic;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

public interface IProductsRepository : IRepository<Product>
{
    Task<List<ProductDataDto>> GetProductDataAsync();
    Task<List<Product>> GetOftenOrderedProducts();
}