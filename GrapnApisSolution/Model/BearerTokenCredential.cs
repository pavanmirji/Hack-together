using Azure.Core;

namespace GrapnApisSolution.Model
{
    public class BearerTokenCredential :  TokenCredential
    {
        private readonly string _bearerToken;
        public BearerTokenCredential(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new AccessToken(_bearerToken, DateTimeOffset.MaxValue);
        }
        public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new ValueTask<AccessToken>(GetToken(requestContext, cancellationToken));
        }
    }
}
