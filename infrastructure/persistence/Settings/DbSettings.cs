namespace persistence.Settings
{
    public class DbSettings: IDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Database { get; set; }

        public string DatabaseType { get; set; }

        public string ConnectionString { 
            get {
                 return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};";
            }  
        }
    }
}