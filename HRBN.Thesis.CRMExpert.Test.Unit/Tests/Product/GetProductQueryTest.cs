using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Product
{
    public class GetProductQueryTest
    {
        [Fact]
        public async Task GetProduct_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var Product = (Domain.Core.Entities.Product) sut.CreateProduct();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ProductsRepository.GetAsync(Product.Id).Returns(Product);

                var query = new GetProductQuery(Product.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetProductQueryHandler(unitOfWorkSubstitute, mapper);
                var ProductQuery = await handler.HandleAsync(query);

                ProductQuery.Id.Should().Be(Product.Id);
            }
        }
    }
}