using System.Collections.Generic;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

public interface ICustomersRepository : IRepository<Customer>
{
    Task<List<CustomerDataDto>> GetCustomerDataAsync();
}