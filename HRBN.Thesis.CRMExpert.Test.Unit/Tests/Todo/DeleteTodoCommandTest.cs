using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Todo
{
    public class DeleteTodoCommandTest
    {
        private readonly IMapper _mapper;

        public DeleteTodoCommandTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
        }

        [Fact]
        public async Task DeleteTodo_WhenItIsPossible_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                Guid guid = Guid.NewGuid();

                var command = new DeleteTodoCommand(guid, guid);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(guid).Returns(new Domain.Core.Entities.Todo());

                var handler = new DeleteTodoCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public async Task DeleteTodo_WhenItIsPossible_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteTodoCommand(Guid.Empty, Guid.Empty);

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(Guid.Empty).ReturnsNull();

                var handler = new DeleteTodoCommandHandler(unitOfWorkSubstitute, _mapper);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().Be(false);

                result.Errors.Count().Should().Be(0);

                result.Message.Should().Be("Todo does not exist.");
            }
        }
    }
}