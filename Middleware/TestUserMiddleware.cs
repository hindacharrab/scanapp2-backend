using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ScanApp2.Middleware
{
    public class TestUserMiddleware
    {
        private readonly RequestDelegate _next;

        public TestUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lire les headers HTTP pour user et site
            var name = context.Request.Headers["X-User-Name"].ToString();
            var site = context.Request.Headers["X-User-Site"].ToString();

            // Valeurs par défaut si non fournies
            if (string.IsNullOrEmpty(name)) name = "aliali";
            if (string.IsNullOrEmpty(site)) site = "Agadir";

            // Créer des Claims pour HttpContext.User
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("site", site)
            };

            var identity = new ClaimsIdentity(claims, "Test");
            context.User = new ClaimsPrincipal(identity);

            await _next(context);
        }
    }
}
