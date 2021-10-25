using System;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Todo
{
    public sealed class GetTodoQuery : IQuery<TodoDto>
    {
        public GetTodoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
