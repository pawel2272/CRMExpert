using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Todo
{
    public sealed class GetTodoQueryHandler : IQueryHandler<GetTodoQuery, TodoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTodoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoDto> HandleAsync(GetTodoQuery query)
        {
            var todo = await _unitOfWork.TodosRepository.GetAsync(query.Id);

            if (todo == null)
            {
                throw new NullReferenceException("Todo does not exist!");
            }

            return _mapper.Map<TodoDto>(todo);
        }
    }
}
