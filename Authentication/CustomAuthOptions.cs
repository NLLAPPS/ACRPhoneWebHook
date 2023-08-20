using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace ACRPhone.Webhook.Authentication
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "custom auth";
        public string Scheme => DefaultScheme;
        public KeyValuePair<string, string> UsernameKeyValue { get; set; }
        public KeyValuePair<string, string> PasswordKeyValue { get; set; }
    }
}