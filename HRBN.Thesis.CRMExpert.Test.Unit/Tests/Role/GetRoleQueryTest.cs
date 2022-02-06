using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Role
{
    public class GetRoleQueryTest
    {
        [Fact]
        public async Task GetRole_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var role = (Domain.Core.Entities.Role) sut.CreateRole();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.RolesRepository.GetAsync(role.Id).Returns(role);

                var query = new GetRoleQuery(role.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetRoleQueryHandler(unitOfWorkSubstitute, mapper);
                var contactQuery = await handler.HandleAsync(query);

                contactQuery.Id.Should().Be(role.Id);
            }
        }
    }
}