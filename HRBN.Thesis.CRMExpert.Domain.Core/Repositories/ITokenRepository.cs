using System.Threading.Tasks;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface ITokenRepository
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}
