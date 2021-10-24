using System.Threading.Tasks;
using AutoMapper;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    public abstract class CommandHandlerBase : ICommandHandler<BaseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommandHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public abstract Task<Result> HandleAsync(BaseCommand command);
    }
}