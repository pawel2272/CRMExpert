using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Order;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using HRBN.Thesis.CRMExpert.Test.Unit.Models;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Order
{
    public class SearchOrderQueryTest
    {
        [Fact]
        public async Task SearchOrder_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var order = (Domain.Core.Entities.Order) sut.CreateOrder();
                ((OrderProxy)order).AddContact(sut.CreateContact());
                var orders = new List<Domain.Core.Entities.Order>() {order};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Order>(orders, 1, 10, 1, null, SortDirection.ASC, "Title");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchOrdersQuery()
                {
                    ContactId = order.ContactId.Value,
                    SearchPhrase = order.Title,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Title",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .OrdersRepository
                    .SearchAsync(query.ContactId, query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchOrdersQueryHandler(unitOfWorkSubstitute, mapper);
                var orderQuery = await handler.HandleAsync(query);

                foreach (var ordr in orderQuery.Items)
                {
                    ordr.Id.Should().Be(order.Id);
                }
            }
        }
    }
}