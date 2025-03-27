namespace DeltaCare.Common
{
    public class DbConnectionString
    {
        public static string ConnectionString { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Expires { get; set; }
    }

    public class EmailConfiguration
    {
        public string EmailConnectionStrings { get; set; }
        public string senderAddress { get; set; }

    }

    public class AppSettings
    {
        public Jwt Jwt { get; set; }
    }
}
