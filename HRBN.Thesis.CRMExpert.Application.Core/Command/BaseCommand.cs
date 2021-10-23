using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    public class BaseCommand : ICommand
    {
        public Guid CommandKey { get; }
    }
}