using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    public abstract class BaseCommand : ICommand
    {
        public Guid CommandKey { get; }
    }
}