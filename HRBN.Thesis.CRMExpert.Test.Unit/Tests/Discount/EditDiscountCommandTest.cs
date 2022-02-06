using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Discount
{
    public class EditDiscountCommandTest
    {
        [Fact]
        public async Task EditDiscount_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditDiscountCommand()
                {
                    Id = Guid.NewGuid(),
                    DiscountVaule = 0.3m,
                    CustomerId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid()
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.DiscountsRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Discount());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditDiscountCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task EditDiscount_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditDiscountCommand()
                {
                    Id = Guid.Empty,
                    DiscountVaule = 0,
                    CustomerId = Guid.Empty,
                    ProductId = Guid.Empty
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.DiscountsRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.Discount());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditDiscountCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(8);
            }
        }
    }
}
