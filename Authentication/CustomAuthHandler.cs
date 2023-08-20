using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;


namespace ACRPhone.Webhook.Authentication
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
    
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {


            // Get Authorization form values
            if (!Request.HasFormContentType ||
                !Request.Form.TryGetValue(Options.UsernameKeyValue.Key, out var username) ||
                !Request.Form.TryGetValue(Options.PasswordKeyValue.Key, out var password))
            {
                return Task.FromResult(AuthenticateResult.Fail("Cannot read authorization values."));
            }

            // Check if username and password are same as configured ones
            if (username.First() != Options.UsernameKeyValue.Value ||
                password.First() != Options.PasswordKeyValue.Value)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));
            }

            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);


            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}