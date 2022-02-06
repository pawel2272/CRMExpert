using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Customer
{
    public class GetCustomerQueryTest
    {
        [Fact]
        public async Task GetCustomer_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var Customer = (Domain.Core.Entities.Customer) sut.CreateCustomer();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.CustomersRepository.GetAsync(Customer.Id).Returns(Customer);

                var query = new GetCustomerQuery(Customer.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetCustomerQueryHandler(unitOfWorkSubstitute, mapper);
                var CustomerQuery = await handler.HandleAsync(query);
                CustomerQuery.Id.Should().Be(Customer.Id);
            }
        }
    }
}