using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Command
{
    /// <summary>
    /// Interface command SQRS.
    /// </summary>
    public interface ICommand
    {
        Guid CommandKey { get; }
    }
}