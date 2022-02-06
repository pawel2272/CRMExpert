using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Discount
{
    public class GetDiscountQueryTest
    {
        [Fact]
        public async Task GetDiscount_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var Discount = (Domain.Core.Entities.Discount) sut.CreateDiscount();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.DiscountsRepository.GetAsync(Discount.Id).Returns(Discount);

                var query = new GetDiscountQuery(Discount.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetDiscountQueryHandler(unitOfWorkSubstitute, mapper);
                var contactQuery = await handler.HandleAsync(query);

                contactQuery.Id.Should().Be(Discount.Id);
            }
        }
    }
}