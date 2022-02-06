using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Product
{
    public class EditProductCommandTest
    {
        [Fact]
        public async Task EditProduct_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditProductCommand()
                {
                    Id = Guid.NewGuid(),
                    Name = "Mikrofalówka",
                    Price = 90m,
                    Description = "Kuchenka mikrofalowa",
                    Type = "Elektronika",
                    Count = 10000
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ProductsRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Product());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditProductCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task EditProduct_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditProductCommand()
                {
                    Id = Guid.Empty,
                    Name = "",
                    Price = 0,
                    Description = "",
                    Type = "",
                    Count = -1
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ProductsRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Product());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditProductCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(4);
            }
        }
    }
}
