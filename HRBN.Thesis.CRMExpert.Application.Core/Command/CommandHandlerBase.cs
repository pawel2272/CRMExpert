namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    public class CommandHandlerBase : ICommandHandler<BaseCommand>
    {
        public Result Handle(BaseCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}