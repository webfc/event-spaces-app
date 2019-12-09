using Ideas.Eventos.Hoteis.Core.Interfaces;
using Ideas.Eventos.Hoteis.Core.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class FullInfoRepository : BaseRepository, IFullInfoRepository
    {
        public HotelFullInfo GetHotel(string id, string country)
        {
            var collection = base._database.GetCollection<HotelFullInfo>("Hotel" + FirstCharToUpper(country));
            return collection.Find(x => x.ofrgId == id).FirstOrDefault();
        }

        public IEnumerable<HotelFullInfo> GetHoteis(FilterModel filter)
        {
            var collection = base._database.GetCollection<HotelFullInfo>("Hotel" + FirstCharToUpper(filter.Country));
            var list = collection.Find(GetFilters(filter));
            var hoteisList = list.Skip(0).Limit(filter.Limit).ToList();
            return hoteisList; 
        }
        public HotelFullInfo UpdateHotel(string id, string country, HotelFullInfo hotel)
        {
            var collection = base._database.GetCollection<HotelFullInfo>("Hotel" + FirstCharToUpper(country));
            var uptHotel = collection.Find(x => x.ofrgId == id).FirstOrDefault();
            uptHotel = hotel;

            collection.ReplaceOne(HotelFullInfo => HotelFullInfo.ofrgId == hotel.ofrgId, uptHotel);

            return uptHotel; 
        }

        public List<dynamic> GetAllCollections()
        {
            List<dynamic> list = new List<dynamic>();
            foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {                
                list.Add(item); 
            }

            return list; 
        }

        public void RemoveHotel(string id, string country)
        {
            var collection = base._database.GetCollection<HotelFullInfo>("Hotel" + FirstCharToUpper(country));
            var _hotel = collection.Find(x => x.ofrgId == id).FirstOrDefault();
            _hotel.DateDeleted = DateTime.Now; 
            collection.ReplaceOne(HotelFullInfo => HotelFullInfo.ofrgId == _hotel.ofrgId, _hotel);
        }
        private FilterDefinition<HotelFullInfo> GetFilters(FilterModel filter)
        {
            var builder = Builders<HotelFullInfo>.Filter;

            var filters = builder.Exists(x => x.ofrgId);
            if (!string.IsNullOrEmpty(filter.City))
                filters = builder.Regex(x => x.basicProfile.city, BsonRegularExpression.Create(new Regex(filter.City, RegexOptions.IgnoreCase))); ;
            if (!string.IsNullOrEmpty(filter.State))
                filters = filters & builder.Regex(x => x.basicProfile.metroAreaName, BsonRegularExpression.Create(new Regex(filter.State, RegexOptions.IgnoreCase)));
            if (filter.Guests != 0)
                filters = filters & builder.Gt(x => x.guestRoom.totalRoomCount, filter.Guests / 2);

            return filters;
        }

    }
}
