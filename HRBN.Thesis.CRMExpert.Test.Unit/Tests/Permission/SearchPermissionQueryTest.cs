using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Permission;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Permission
{
    public class SearchPermissionQueryTest
    {
        [Fact]
        public async Task SearchPermission_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var permission = (Domain.Core.Entities.Permission) sut.CreatePermission();
                var permissions = new List<Domain.Core.Entities.Permission>() {permission};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Permission>(permissions, 1, 10, 1, null, SortDirection.ASC, "Name");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchPermissionsQuery()
                {
                    SearchPhrase = null,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Name",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .PermissionsRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchPermissionsQueryHandler(unitOfWorkSubstitute, mapper);
                var permissionQuery = await handler.HandleAsync(query);

                foreach (var permissione in permissionQuery.Items)
                {
                    permissione.Id.Should().Be(permission.Id);
                }
            }
        }
    }
}