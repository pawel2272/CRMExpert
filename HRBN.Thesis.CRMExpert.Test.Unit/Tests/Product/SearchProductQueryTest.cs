using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Product
{
    public class SearchProductQueryTest
    {
        [Fact]
        public async Task SearchProduct_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var product = (Domain.Core.Entities.Product) sut.CreateProduct();
                var products = new List<Domain.Core.Entities.Product>() {product};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Product>(products, 1, 10, 1, null, SortDirection.ASC, "Title");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchProductsQuery()
                {
                    SearchPhrase = product.Name,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Title",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .ProductsRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchProductsQueryHandler(unitOfWorkSubstitute, mapper);
                var ProductQuery = await handler.HandleAsync(query);

                foreach (var ordr in ProductQuery.Items)
                {
                    ordr.Id.Should().Be(product.Id);
                }
            }
        }
    }
}