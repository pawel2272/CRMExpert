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
    public class LoginCommandTest
    {
        private readonly IMapper _mapper;

        public LoginCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
        }

        [Fact]
        public async Task Login_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = (Domain.Core.Entities.User) sut.CreateUser();

                var command = new LoginCommand()
                {
                    Username = "admin",
                    Password = "password",
                    RememberMe = true
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);
                unitOfWorkSubstitute.UsersRepository.LoginAsync(command.Username, command.Password, command.RememberMe)
                    .Returns("asd");

                var handler = new LoginCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task Login_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = (Domain.Core.Entities.User) sut.CreateUser();

                var command = new LoginCommand()
                {
                    Username = "",
                    Password = "",
                    RememberMe = true
                };

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);
                unitOfWorkSubstitute.UsersRepository.LoginAsync(command.Username, command.Password, command.RememberMe)
                    .Returns("asd");

                var handler = new LoginCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(2);
            }
        }
    }
}