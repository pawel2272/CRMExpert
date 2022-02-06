using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class ContactsRepository : IContactsRepository
    {
        private readonly CRMContext _dbContext;

        public ContactsRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Contact entity)
        {
            await _dbContext.Contacts.AddAsync(entity);
        }

        public async Task DeleteAsync(Contact entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Contacts.Remove(entity); });
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id.Equals(id));
            return contact;
        }

        private async Task<IPageResult<Contact>> ProcessSearchQueryAsync(IQueryable<Contact> baseQuery, int pageNumber,
            int pageSize,
            string orderBy,
            SortDirection sortDirection,
            string searchPhrase)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Contact, object>>>()
                {
                    {nameof(Contact.FirstName), e => e.FirstName},
                    {nameof(Contact.LastName), e => e.LastName},
                    {nameof(Contact.Phone), e => e.Phone},
                    {nameof(Contact.Email), e => e.Email},
                    {nameof(Contact.Street), e => e.Street},
                    {nameof(Contact.PostalCode), e => e.PostalCode},
                    {nameof(Contact.City), e => e.City},
                    {nameof(Contact.ContactComment), e => e.ContactComment},
                    {nameof(Contact.CreDate), e => e.CreDate},
                    {nameof(Contact.ModDate), e => e.ModDate}
                };

                Expression<Func<Contact, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["FirstName"];
                }

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Contact>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase,
                sortDirection, orderBy);
        }

        public async Task<IPageResult<Contact>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
            string orderBy,
            SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Contacts
                .Where(e => (searchPhrase == null ||
                             e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             || e.FirstName.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.LastName.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Phone.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Email.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Street.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.PostalCode.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.City.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.ContactComment.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.CustomerId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             || e.UserId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                    ));

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task<IPageResult<Contact>> SearchAsync(Guid userId, string searchPhrase, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Contacts
                .Where(e => (searchPhrase == null ||
                             (e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                              || e.FirstName.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.LastName.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.Phone.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.Email.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.Street.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.PostalCode.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.City.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.ContactComment.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.UserId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                              || e.CustomerId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             )) && e.UserId == userId);

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task<List<ContactDataDto>> GetContactDataAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                var results = _dbContext.Contacts
                    .OrderBy(e => e.FirstName)
                    .Select(e => new ContactDataDto()
                        {Id = e.Id, Name = $"{e.FirstName} {e.LastName}"})
                    .ToList();
                return results;
            });
        }

        public async Task<List<ContactDataDto>> GetContactDataAsyncByCustomer(Guid customerId)
        {
            return await Task.Factory.StartNew(() =>
            {
                var results = _dbContext.Contacts.Where(e => e.CustomerId == customerId)
                    .OrderBy(e => e.FirstName)
                    .Select(e =>
                        new ContactDataDto() {Id = e.Id, Name = $"{e.FirstName} {e.LastName}"})
                    .ToList();
                return results;
            });
        }

        public async Task<List<ContactDataDto>> GetContactDataAsyncByUser(Guid userId)
        {
            return await Task.Factory.StartNew(() =>
            {
                var results = _dbContext.Contacts.Where(e => e.UserId == userId)
                    .OrderBy(e => e.FirstName)
                    .Select(e =>
                        new ContactDataDto() {Id = e.Id, Name = $"{e.FirstName} {e.LastName}"})
                    .ToList();
                return results;
            });
        }

        public async Task<List<Contact>> GetContactByUserAsync(Guid userId)
        {
            var result = await _dbContext.Contacts
                .Where(e => e.UserId.Equals(userId))
                .ToListAsync();

            return result;
        }

        public async Task UpdateAsync(Contact entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Contacts.Update(entity); });
        }
    }
}