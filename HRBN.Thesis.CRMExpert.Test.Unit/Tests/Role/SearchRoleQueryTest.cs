using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Role
{
    public class SearchRoleQueryTest
    {
        [Fact]
        public async Task SearchRole_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var role = (Domain.Core.Entities.Role) sut.CreateRole();
                var roles = new List<Domain.Core.Entities.Role>() {role};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Role>(roles, 1, 10, 1, null, SortDirection.ASC, "Name");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchRolesQuery()
                {
                    SearchPhrase = role.Name,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Name",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .RolesRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchRolesQueryHandler(unitOfWorkSubstitute, mapper);
                var roleQuery = await handler.HandleAsync(query);

                foreach (var rolee in roleQuery.Items)
                {
                    rolee.Id.Should().Be(role.Id);
                }
            }
        }
    }
}