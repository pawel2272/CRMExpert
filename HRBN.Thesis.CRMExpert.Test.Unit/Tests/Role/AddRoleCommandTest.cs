using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Role
{
    public class AddRoleCommandTest
    {
        [Fact]
        public async Task AddRole_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddRoleCommand()
                {
                    Name = "Sample"
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task AddRole_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddRoleCommand()
                {
                    Name = ""
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(1);
            }
        }
    }
}
