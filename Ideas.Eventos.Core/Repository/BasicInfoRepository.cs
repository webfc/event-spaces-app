using Ideas.Eventos.Hoteis.Core.Interfaces;
using Ideas.Eventos.Hoteis.Core.Model;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class BasicInfoRepository : BaseRepository, IBasicInfoRepository
    {
        public object GetDocuments(FilterModel filter)
        {
            var collection = base._database.GetCollection<HotelBasic>("Venues" + FirstCharToUpper(filter.Country));
            var query = collection.Find(GetFilters(filter));
            long numberOfHotels = query.CountDocuments();
            var hotelList = query.Skip(filter.From).Limit(filter.Limit).ToList();
            return new { numberOfHotels, hotelList };
        }

        public object GetAccorDocuments(FilterModel filter)
        {
            var collection = base._database.GetCollection<HotelBasic>("Venues" + FirstCharToUpper(filter.Country));
            var query = collection.Find(GetFilters(filter) & Builders<HotelBasic>.Filter.Regex(x => x.chainName, BsonRegularExpression.Create("Accor")));
            long numberOfHotels = query.CountDocuments();
            var hotelList = query.Skip(filter.From).Limit(filter.Limit).ToList();
            return new { numberOfHotels, hotelList };
        }


        private FilterDefinition<HotelBasic> GetFilters(FilterModel filter)
        {
            var builder = Builders<HotelBasic>.Filter;

            var filters = builder.Exists(x => x.name);
            if (!string.IsNullOrEmpty(filter.City))
               filters = builder.Regex(x => x.city, BsonRegularExpression.Create(new Regex(filter.City, RegexOptions.IgnoreCase)));
            if (!string.IsNullOrEmpty(filter.State))
                filters = filters & builder.Regex(x => x.metroAreaName, BsonRegularExpression.Create(new Regex(filter.State, RegexOptions.IgnoreCase)));
            if (filter.Guests != 0)
                filters = filters & builder.Gt(x => x.totalSleepingRoom, filter.Guests / 2);
            if (filter.MetricValue != 0)
                filters = filters & builder.Gt(x => x.largestMeetingSpace.metricValue, filter.MetricValue);            

            filters = filters & builder.Where(x => x.DateDeleted == null); 
            return filters;
        }
    }
}
