namespace HRBN.Thesis.CRMExpert.Application.Core.Context
{
    public interface IIdentityContext
    {
        public string UserID { get; set; }
        public bool HasPermissions { get; set; }
    }
}