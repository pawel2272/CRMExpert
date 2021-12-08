namespace HRBN.Thesis.CRMExpert.Application.Core.Context
{
    /// <summary>
    /// Application context.
    /// </summary>
    public interface IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
    }
}