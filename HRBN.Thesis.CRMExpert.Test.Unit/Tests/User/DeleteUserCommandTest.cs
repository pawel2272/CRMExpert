using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.User
{
    public class DeleteUserCommandTest
    {
        private readonly IMapper _mapper;

        public DeleteUserCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
        }

        [Fact]
        public async Task DeleteUser_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteUserCommand(guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(guid).Returns(new Domain.Core.Entities.User());

                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteUser_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteUserCommand(Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.UsersRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("User does not exist.");
            }
        }
    }
}