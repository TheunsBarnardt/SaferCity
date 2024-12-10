namespace Aura.PersonalSafety.Handlers
{
    internal class UnauthorizedHandler : DelegatingHandler
    {
        internal UnauthorizedHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
