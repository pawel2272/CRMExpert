using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    public abstract class CommandHandlerBase<T> : ICommandHandler<T> where T : BaseCommand
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected CommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public abstract Task<Result> HandleAsync(T command);
    }
}