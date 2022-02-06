using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Todo;
using HRBN.Thesis.CRMExpert.Application.Mappers.Profiles;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using NSubstitute;
using Xunit;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Tests.Todo
{
    public class SearchTodoQueryTest
    {
        [Fact]
        public async Task SearchTodo_WhenItExists_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var todo = (Domain.Core.Entities.Todo) sut.CreateTodo();
                var todos = new List<Domain.Core.Entities.Todo>() {todo};
                var pageResult =
                    new PageResult<Domain.Core.Entities.Todo>(todos, 1, 10, 1, null, SortDirection.ASC, "Title");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

                var query = new SearchTodosQuery()
                {
                    ContactId = todo.ContactId.Value,
                    SearchPhrase = todo.Title,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = "Title",
                    SortDirection = SortDirection.DESC
                };

                unitOfWorkSubstitute
                    .TodosRepository
                    .SearchAsync(query.SearchPhrase, query.PageNumber, query.PageSize, query.OrderBy,
                        query.SortDirection)
                    .Returns(pageResult);

                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new SearchTodosQueryHandler(unitOfWorkSubstitute, mapper);
                var todoQuery = await handler.HandleAsync(query);

                foreach (var tod in todoQuery.Items)
                {
                    tod.Id.Should().Be(todo.Id);
                }
            }
        }
    }
}