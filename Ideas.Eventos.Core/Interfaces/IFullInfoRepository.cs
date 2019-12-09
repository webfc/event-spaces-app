using Ideas.Eventos.Hoteis.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Core.Interfaces
{
    public interface IFullInfoRepository
    {
        HotelFullInfo GetHotel(string id, string country);
        IEnumerable<HotelFullInfo> GetHoteis(FilterModel filter);
        HotelFullInfo UpdateHotel(string id, string country, HotelFullInfo hotel);
        void RemoveHotel(string id, string country); 
    }
}
