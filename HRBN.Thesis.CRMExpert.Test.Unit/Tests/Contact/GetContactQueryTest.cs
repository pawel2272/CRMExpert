using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Contact
{
    public class GetContactQueryTest
    {
        [Fact]
        public async Task GetContact_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var contact = (Domain.Core.Entities.Contact) sut.CreateContact();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.ContactsRepository.GetAsync(contact.Id).Returns(contact);

                var query = new GetContactQuery(contact.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetContactQueryHandler(unitOfWorkSubstitute, mapper);
                var contactQuery = await handler.HandleAsync(query);
                contactQuery.Id.Should().Be(contact.Id);
            }
        }
    }
}