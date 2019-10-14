using MongoDB.Driver;
using System;
using System.Security.Authentication;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class BaseRepository
    {
        protected readonly IMongoDatabase _database;
        public BaseRepository()
        {
            MongoClientSettings settings = MongoClientSettings
                          .FromUrl(new MongoUrl("mongodb://ideasfractal:sNHQ3aKRIHUs2pST@ideasfractal-shard-00-00-kal8b.gcp.mongodb.net:27017,ideasfractal-shard-00-01-kal8b.gcp.mongodb.net:27017,ideasfractal-shard-00-02-kal8b.gcp.mongodb.net:27017/test?ssl=true&replicaSet=IdeasFractal-shard-0&authSource=admin&retryWrites=true&w=majority"));
            //   .FromUrl(new MongoUrl("mongodb://localhost:27017"));

            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 }; settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);
            settings.ConnectTimeout = TimeSpan.FromSeconds(60);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("CrawlerHoteis");
        }

        protected string FirstCharToUpper(string entrada)
        {
            return char.ToUpper(entrada[0]) + entrada.Substring(1);
        }
    }
}
