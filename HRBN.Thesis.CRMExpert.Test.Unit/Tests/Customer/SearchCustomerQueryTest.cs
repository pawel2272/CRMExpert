using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Customer
{
    public class SearchCustomerQueryTest
    {
        [Fact]
        public async Task SearchCustomer_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var customer = (Domain.Core.Entities.Customer) sut.CreateCustomer();
                var customers = new List<Domain.Core.Entities.Customer>() {customer};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Customer>(customers, 1, 10, 1, null, SortDirection.ASC,
                        "FirstName");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchCustomersQuery()
                {
                    SearchPhrase = customer.Name,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "FirstName",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .CustomersRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchCustomersQueryHandler(unitOfWorkSubstitute, mapper);
                var CustomerQuery = await handler.HandleAsync(query);

                foreach (var cntct in CustomerQuery.Items)
                {
                    cntct.Id.Should().Be(customer.Id);
                }
            }
        }
    }
}