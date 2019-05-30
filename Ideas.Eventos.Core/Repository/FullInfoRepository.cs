using Ideas.Eventos.Hoteis.Core.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class FullInfoRepository : BaseRepository
    {
        public HotelFullInfo GetHotel(string id, string country)
        {
            var collection = base._database.GetCollection<HotelFullInfo>("Hotel" + FirstCharToUpper(country));
            return collection.Find(x => x.ofrgId == id).FirstOrDefault();
        }
    }
}
