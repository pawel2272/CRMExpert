using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Order
{
    public class AddOrderCommandTest
    {
        [Fact]
        public async Task AddOrder_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderCommand()
                {
                    Title = "Test",
                    Content = "Test",
                    ContactId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    Price = 3.14m
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ProductsRepository.GetAsync(command.ProductId).Returns(new Product());
                unitOfWorkSubstitute.CustomersRepository.GetCustomerByContactIdAsync(command.ContactId)
                    .Returns(new Customer());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddOrder_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderCommand()
                {
                    Title = "",
                    Content = "",
                    ContactId = Guid.Empty,
                    Price = 0
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(6);
            }
        }
    }
}