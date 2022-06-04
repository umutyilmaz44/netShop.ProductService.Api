namespace persistence.Settings
{
    public interface IDbSettings
    {
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Database { get; set; }

        public string ConnectionString { get; }
    }
}