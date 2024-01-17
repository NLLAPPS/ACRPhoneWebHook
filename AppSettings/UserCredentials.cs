namespace ACRPhoneWebHook.AppSettings
{
    public class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string Secret { get; set; }
    }
}
