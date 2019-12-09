using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Eventos.Hoteis.Crawler
{
    public interface ICrawlerService
    {
        void UpdateDB(string local);
        void UpdateBrazilianPlaces();
        Task<string> UpdateByCountry(string pais, int Take);
        void GenerateJsonPlaces(); 
    }

}
