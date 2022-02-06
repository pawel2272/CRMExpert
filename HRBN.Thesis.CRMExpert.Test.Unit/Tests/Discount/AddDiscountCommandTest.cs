using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Discount
{
    public class AddDiscountCommandTest
    {
        [Fact]
        public async Task AddDiscount_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddDiscountCommand()
                {
                    DiscountVaule = 0.3m,
                    CustomerId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid()
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddDiscountCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddDiscount_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddDiscountCommand()
                {
                    DiscountVaule = 0,
                    CustomerId = Guid.Empty,
                    ProductId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddDiscountCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(6);
            }
        }
    }
}
