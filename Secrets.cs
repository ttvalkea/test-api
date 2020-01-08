namespace TestApi
{
    public static class Secrets
    {
        //These will get their values from Azure Key Vault on startup
        public static string DbUserName { get; set; }
        public static string DbUserPassword { get; set; }
    }
}
