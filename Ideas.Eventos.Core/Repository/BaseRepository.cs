using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class BaseRepository
    {
        protected readonly IMongoDatabase _database;
        public BaseRepository()
        {
            MongoClientSettings settings = MongoClientSettings
                .FromUrl(new MongoUrl("mongodb+srv://ideasfractal:sNHQ3aKRIHUs2pST@ideasfractal-kal8b.gcp.mongodb.net/test"));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 }; settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);

            var client = new MongoClient(settings);
            _database = client.GetDatabase("Hoteis");
        }

        protected string FirstCharToUpper(string entrada)
        {
            return char.ToUpper(entrada[0]) + entrada.Substring(1);
        }
    }
}
