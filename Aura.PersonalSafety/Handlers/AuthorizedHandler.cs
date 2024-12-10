using Aura.PersonalSafety.Services;

namespace Aura.PersonalSafety.Handlers
{
    internal class AuthorizedHandler : DelegatingHandler
    {
        private readonly AuthService _authService;

        internal AuthorizedHandler(AuthService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = string.Empty;

            if (request.RequestUri.AbsolutePath.Contains("callout"))
            {
                token = await _authService.GetCalloutTokenAsync(); // Use Callout Token
            }
            else
            {
                token = await _authService.GetServerTokenAsync(); // Use Server Token
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
