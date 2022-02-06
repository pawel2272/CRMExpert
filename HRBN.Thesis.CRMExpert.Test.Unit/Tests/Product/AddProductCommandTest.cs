using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Product
{
    public class AddProductCommandTest
    {
        [Fact]
        public async Task AddProduct_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddProductCommand()
                {
                    Name = "Mikrofalówka",
                    Price = 90m,
                    Description = "Kuchenka mikrofalowa",
                    Type = "Elektronika",
                    Count = 10000
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddProductCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddProduct_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddProductCommand()
                {
                    Name = "",
                    Price = 0,
                    Description = "",
                    Type = "",
                    Count = -1
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddProductCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(2);
            }
        }
    }
}