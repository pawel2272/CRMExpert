using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.User
{
    public class LogoutCommandTest
    {
        [Fact]
        public async Task Logout_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = (Domain.Core.Entities.User) sut.CreateUser();

                var command = new LogoutCommand();

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);

                var handler = new LogoutCommandHandler(unitOfWorkSubstitute);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }
    }
}