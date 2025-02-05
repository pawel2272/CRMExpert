﻿using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Order;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Order
{
    public class GetOrderQueryTest
    {
        [Fact]
        public async Task GetOrder_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var order = (Domain.Core.Entities.Order) sut.CreateOrder();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.OrdersRepository.GetAsync(order.Id).Returns(order);

                var query = new GetOrderQuery(order.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetOrderQueryHandler(unitOfWorkSubstitute, mapper);
                var orderQuery = await handler.HandleAsync(query);

                orderQuery.Id.Should().Be(order.Id);
            }
        }
    }
}