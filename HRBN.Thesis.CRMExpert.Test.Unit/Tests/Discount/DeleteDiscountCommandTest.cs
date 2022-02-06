using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Discount
{
    public class DeleteDiscountCommandTest
    {
        private readonly IMapper _mapper;

        public DeleteDiscountCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
        }

        [Fact]
        public async Task DeleteDiscount_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteDiscountCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.DiscountsRepository.GetAsync(guid).Returns(new Domain.Core.Entities.Discount());

                var handler = new DeleteDiscountCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteDiscount_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteDiscountCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.DiscountsRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteDiscountCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Discount does not exist.");
            }
        }
    }
}