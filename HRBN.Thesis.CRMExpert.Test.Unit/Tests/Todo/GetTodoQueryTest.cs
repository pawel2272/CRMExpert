using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Todo;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Todo
{
    public class GetTodoQueryTest
    {
        [Fact]
        public async Task GetTodo_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var todo = (Domain.Core.Entities.Todo) sut.CreateTodo();
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                unitOfWorkSubstitute.TodosRepository.GetAsync(todo.Id).Returns(todo);

                var query = new GetTodoQuery(todo.Id);
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new GetTodoQueryHandler(unitOfWorkSubstitute, mapper);
                var todoQuery = await handler.HandleAsync(query);

                todoQuery.Id.Should().Be(todo.Id);
            }
        }
    }
}