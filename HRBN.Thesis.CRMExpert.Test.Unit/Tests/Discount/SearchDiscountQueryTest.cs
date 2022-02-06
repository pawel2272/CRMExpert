using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Discount
{
    public class SearchDiscountQueryTest
    {
        [Fact]
        public async Task SearchDiscount_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var discount = (Domain.Core.Entities.Discount) sut.CreateDiscount();
                var discounts = new List<Domain.Core.Entities.Discount>() {discount};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Discount>(discounts, 1, 10, 1, null, SortDirection.ASC, "Name");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchDiscountsQuery()
                {
                    SearchPhrase = discount.DiscountVaule.ToString(),
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Name",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .DiscountsRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchDiscountsQueryHandler(unitOfWorkSubstitute, mapper);
                var discountQuery = await handler.HandleAsync(query);

                foreach (var discounte in discountQuery.Items)
                {
                    discounte.Id.Should().Be(discount.Id);
                }
            }
        }
    }
}