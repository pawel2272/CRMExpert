using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.User
{
    public class GetUserQueryTest
    {
        [Fact]
        public async Task GetUser_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = (Domain.Core.Entities.User) sut.CreateUser();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);

                var query = new GetUserQuery(user.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetUserQueryHandler(unitOfWorkSubstitute, mapper);
                var userQuery = await handler.HandleAsync(query);

                userQuery.Id.Should().Be(user.Id);
            }
        }
    }
}