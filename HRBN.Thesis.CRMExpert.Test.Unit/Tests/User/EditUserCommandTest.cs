using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.User
{
    public class EditUserCommandTest
    {
        [Fact]
        public async Task EditUser_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditUserCommand()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Gender = "m",
                    Password = "adminadminadmin",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Phone = "123456789",
                    Email = "jan@kowalski.pl",
                    Street = "Miodowa 12",
                    PostalCode = "00-000",
                    City = "Warszawa",
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.User());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditUserCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task EditUser_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditUserCommand()
                {
                    Id = Guid.Empty,
                    Username = "",
                    Gender = "",
                    Password = "",
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Email = "",
                    Street = "",
                    PostalCode = "",
                    City = "",
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(command.Id).Returns(new Domain.Core.Entities.User());

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));

                var handler = new EditUserCommandHandler(unitOfWorkSubstitute, mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(11);
            }
        }
    }
}
