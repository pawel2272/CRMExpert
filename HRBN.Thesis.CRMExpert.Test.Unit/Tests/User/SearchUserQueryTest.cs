﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.User
{
    public class SearchUserQueryTest
    {
        [Fact]
        public async Task SearchUser_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = (Domain.Core.Entities.User) sut.CreateUser();
                var users = new List<Domain.Core.Entities.User>() {user};
                var pageResult =
                    new PageResult<Domain.Core.Entities.User>(users, 1, 10, 1, null, SortDirection.ASC, "Username");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchUsersQuery()
                {
                    SearchPhrase = user.FirstName,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "FirstName",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .UsersRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchUsersQueryHandler(unitOfWorkSubstitute, mapper);
                var UserQuery = await handler.HandleAsync(query);

                foreach (var usr in UserQuery.Items)
                {
                    usr.Id.Should().Be(user.Id);
                }
            }
        }
    }
}