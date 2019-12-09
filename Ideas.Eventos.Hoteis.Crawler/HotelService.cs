using Ideas.Eventos.Hoteis.Core.Repository;
using Ideas.Eventos.Hoteis.Crawler.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using static Ideas.Eventos.Hoteis.Crawler.Model.Search;

namespace Ideas.Eventos.Hoteis.Crawler
{
    public class HotelService
    {
        private readonly IMongoCollection<VenueList> _venues;
        private readonly IMongoCollection<Hotel> _hotels;
        private readonly IMongoCollection<Log> _log;
        private string _local { get; set; }

        public HotelService(string local)
        {
            _local = local;
            MongoClientSettings settings = MongoClientSettings
               .FromUrl(new MongoUrl("mongodb://ideasfractal:sNHQ3aKRIHUs2pST@ideasfractal-shard-00-00-kal8b.gcp.mongodb.net:27017,ideasfractal-shard-00-01-kal8b.gcp.mongodb.net:27017,ideasfractal-shard-00-02-kal8b.gcp.mongodb.net:27017/test?ssl=true&replicaSet=IdeasFractal-shard-0&authSource=admin&retryWrites=true&w=majority"));
            //   .FromUrl(new MongoUrl("mongodb://localhost:27017"));

            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 }; settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);
            settings.ConnectTimeout = TimeSpan.FromSeconds(60);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("CrawlerHoteis");
            _venues = database.GetCollection<VenueList>("Venues" + _local);
            _hotels = database.GetCollection<Hotel>("Hotel" + _local);
            _log = database.GetCollection<Log>("Log");
        }

        public List<VenueList> Get()
        {
            try
            {
                return _venues.Find(VenueList => true).ToList();
            }
            catch (System.Exception ex)
            {
                var log = new Log { IdObjeto = "", Exception = ex.Message, Metodo = "Get Venues", TipoObjeto = "Venue" };
                CreateLog(log);
                throw;
            }
        }

        public VenueList Get(string id)
        {
            return _venues.Find<VenueList>(VenueList => VenueList.id == id).FirstOrDefault();
        }

        public VenueList Create(VenueList VenueList)
        {
            try
            {
                _venues.InsertOne(VenueList);

            }
            catch (System.Exception ex)
            {
                var log = new Log { IdObjeto = VenueList.id, Exception = ex.InnerException.ToString(), Metodo = "Create Venues", TipoObjeto = "Venue" };
                CreateLog(log);
                return VenueList;
            }
            return VenueList;
        }

        public void Update(string id, VenueList VenueListIn)
        {
            _venues.ReplaceOne(VenueList => VenueList.id == id, VenueListIn);
        }

        public void Remove(VenueList VenueListIn)
        {
            _venues.DeleteOne(VenueList => VenueList.id == VenueListIn.id);
        }

        public void Remove(string id)
        {
            _venues.DeleteOne(VenueList => VenueList.id == id);
        }

        public List<Hotel> GetHotels()
        {
            try
            {
                return _hotels.Find(Hotel => true).ToList();

            }
            catch (System.Exception ex)
            {
                var log = new Log { IdObjeto = "", Exception = ex.Message, Metodo = "Get hotel", TipoObjeto = "hotel" };
                CreateLog(log);
                return new List<Hotel>();
            }
        }
        public List<string> GetCities()
        {
            return _hotels.AsQueryable<Hotel>().Select(e => e.basicProfile.city).Distinct().ToList();
        }
        public List<string> GetStates()
        {
            return _hotels.AsQueryable<Hotel>().Select(e => e.basicProfile.stateProvinceCode).Distinct().ToList();
        }
        public List<string> GetCountry()
        {
            return _hotels.AsQueryable<Hotel>().Select(e => e.basicProfile.countryCode).Distinct().ToList();
        }
        public Hotel GetHotel(string id)
        {
            return _hotels.Find<Hotel>(hotel => hotel.ofrgId == id).FirstOrDefault();
        }

        public Hotel CreateHotel(Hotel hotel)
        {
            try
            {
                _hotels.InsertOne(hotel);
                return hotel;
            }
            catch (System.Exception ex)
            {
                var log = new Log { IdObjeto = hotel.ofrgId, Exception = ex.InnerException.ToString(), Metodo = "Create Hotel", TipoObjeto = "Hotel" };
                CreateLog(log);
                return null;
            }

        }

        public Hotel UpdateHotel(string id, Hotel _hotel)
        {
            try
            {
                _hotels.ReplaceOne(hotel => hotel.ofrgId == id, _hotel);
                return _hotel;
            }
            catch (System.Exception ex)
            {
                var log = new Log { IdObjeto = _hotel.ofrgId, Exception = ex.InnerException.ToString(), Metodo = "Update Hotel", TipoObjeto = "Hotel" };
                CreateLog(log);
                return null;
            }

        }

        public object GetAllCollections()
        {
            FullInfoRepository repository = new FullInfoRepository();
            var collections = repository.GetAllCollections();
            return collections; 
        }
        public void CreateLog(Log log)
        {
            _log.InsertOne(log);
        }
    }
}
