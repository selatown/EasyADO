namespace EasyADO
{
    public static class SQLServerConnection
    {
        public static string GetConnectionString(string ServerName, string DatabaseName, string UserName, string Password)
        {
            return $"Server={ServerName};Database={DatabaseName};User Id={UserName};Password={Password}";
        }
    }
}
