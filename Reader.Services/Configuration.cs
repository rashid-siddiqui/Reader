namespace Reader.Services
{
    using System.Collections.Specialized;
    using MongoDB.Driver;

    public static class Configuration
    {
        internal static MongoDatabase Database { get; private set; }

        public static void Initialize(NameValueCollection appSettings)
        {
            Configuration.ConfigureDatabase(appSettings["MONGOLAB_URI"]);
        }

        private static void ConfigureDatabase(string connection_string)
        {
            var url = new MongoUrl(connection_string);
            var client = new MongoClient(url);
            var server = client.GetServer();
            Configuration.Database = server.GetDatabase(url.DatabaseName);
        }
    }
}