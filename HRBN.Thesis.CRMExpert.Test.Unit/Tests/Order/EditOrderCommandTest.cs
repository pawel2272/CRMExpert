﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Order
{
    public class EditOrderCommandTest
    {
        [Fact]
        public async Task EditOrder_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditOrderCommand()
                {
                    Id = Guid.NewGuid(),
                    Title = "Test",
                    Content = "Test",
                    ContactId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    Price = 3.14m
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.OrdersRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Order());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task EditOrder_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditOrderCommand()
                {
                    Id = Guid.Empty,
                    Title = "",
                    Content = "",
                    ContactId = Guid.Empty,
                    Price = 0
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.OrdersRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Order());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditOrderCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(8);
            }
        }
    }
}
