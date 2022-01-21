using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class OrdersRepository : IOrdersRepository
    {
        private readonly CRMContext _dbContext;

        public OrdersRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IPageResult<Order>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy,
            SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            await Task.Factory.StartNew(() => { _dbContext.Orders.Remove(order); });
        }

        public async Task<Order> GetAsync(Guid id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<IPageResult<Order>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Orders
                .Where(o => o.ContactId == contactId
                            && (searchPhrase == null ||
                                o.Title.ToLower().Contains(searchPhrase.ToLower())
                                || o.Content.ToLower().Contains(searchPhrase.ToLower())));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>()
                {
                    {nameof(Order.Title), o => o.Title},
                    {nameof(Order.Content), o => o.Content}
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new PageResult<Order>(orders, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Order order)
        {
            await Task.Factory.StartNew(() => { _dbContext.Orders.Update(order); });
        }
    }
}