using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Sukalibur.Graph
{
    public class ErrorFilter : IErrorFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ErrorFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IError OnError(IError error)
        {
            if (error.Code == "AUTH_NOT_AUTHENTICATED" && _httpContextAccessor.HttpContext?.Items["JwtAuthenticationFailure"] is SecurityTokenExpiredException)
            {
                return ErrorBuilder.New().SetCode("AUTH_TOKEN_EXPIRED").SetMessage("Access token already expired").Build();
            }
            return error;
        }
    }
}
