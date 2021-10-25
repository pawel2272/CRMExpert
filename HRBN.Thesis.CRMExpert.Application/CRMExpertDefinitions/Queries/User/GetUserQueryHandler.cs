using System;
using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Application.Core.Query;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User
{
    public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(query.Id);

            if (user == null)
            {
                throw new NullReferenceException("User does not exist!");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
