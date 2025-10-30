using ScanApp2.Services.Interfaces;
using System.Security.Claims;

namespace ScanApp2.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.Name)?.Value
                   ?? user?.Identity?.Name
                   ?? "Utilisateur inconnu";
        }

        public string GetCurrentUserSite()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst("site")?.Value
                   ?? "Site non défini";
        }
    }
}
