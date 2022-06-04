namespace webApi.Settings
{
    public class SsoSettings : ISsoSettings
    {
        public string Authority { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}