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

        public async Task AddAsync(Order entity)
        {
            await _dbContext.Orders.AddAsync(entity);
        }

        public async Task DeleteAsync(Order entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Orders.Remove(entity); });
        }

        public async Task<Order> GetAsync(Guid id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(e => e.Id == id);
            return order;
        }

        private async Task<IPageResult<Order>> ProcessSearchQueryAsync(IQueryable<Order> baseQuery, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection,
            string searchPhrase)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>()
                {
                    {nameof(Order.Title), e => e.Title},
                    {nameof(Order.Content), e => e.Content},
                    {nameof(Order.CreDate), e => e.CreDate},
                    {nameof(Order.ModDate), e => e.ModDate}
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new PageResult<Order>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase, sortDirection, orderBy);
        }

        public async Task<IPageResult<Order>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Orders
                .Where(e => e.ContactId == contactId
                            && (searchPhrase == null ||
                                e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                                || e.Title.ToLower().Contains(lowerCaseSearchPhrase)
                                || e.Content.ToLower().Contains(lowerCaseSearchPhrase)
                                || e.ContactId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                                || e.ProductId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                            ));

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task<List<Order>> GetLastOrders(int daysFromToday)
        {

            var orders = await _dbContext.Orders
                .Where(o => o.CreDate.CompareTo(DateTime.Now.AddDays(-daysFromToday)) > 0)
                .Include(e => e.Contact)
                .Include(e => e.Product)
                .ToListAsync();

            return orders;
        }

        public async Task<IPageResult<Order>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
            string orderBy,
            SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Orders
                .Where(e => searchPhrase == null ||
                            e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                            || e.Title.ToLower().Contains(lowerCaseSearchPhrase)
                            || e.Content.ToLower().Contains(lowerCaseSearchPhrase)
                            || e.ContactId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                            || e.ProductId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                );

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task UpdateAsync(Order entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Orders.Update(entity); });
        }
    }
}