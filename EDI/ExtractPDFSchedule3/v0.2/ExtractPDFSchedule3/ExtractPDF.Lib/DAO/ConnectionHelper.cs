namespace ExtractPDF.Lib.DAO
{
    public static class ConnectionHelper
    {
        public static string ConnectionString =
            "Server=tcp:fishinabox.database.windows.net,1433;Initial Catalog=FIAB;Persist Security Info=False;" +
            "User ID=FIAB;Password=Monday99;" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
