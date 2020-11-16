using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}