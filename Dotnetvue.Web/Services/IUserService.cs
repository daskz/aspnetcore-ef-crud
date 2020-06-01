using Dotnetvue.Data.Models;
using Dotnetvue.Web.Models;

namespace Dotnetvue.Web.Services
{
    public interface IUserService
    {
        AuthResponse Authenticate(AuthRequest request);
        User Create(User user, string password);
        User GetCurrentUser();
    }
}