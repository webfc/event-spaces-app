using GoogleTranslateFreeApi;
using Ideas.Eventos.Hoteis.Core.Repository;
using Ideas.Eventos.Hoteis.Crawler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ideas.Eventos.Hoteis.Crawler.Model.Search;

namespace Ideas.Eventos.Hoteis.Crawler
{
    public class CrawlerService : ICrawlerService
    {
        private static List<VenueList> _hotelBasic { get; set; }
        private static HotelService _service { get; set; }
        private static string _local { get; set; }
        public void UpdateDB(string local)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
          
            if (local == "u")
            {
                var estados = Helper.InitializeUsList();
                foreach (var estado in estados)
                {

                    _service = new HotelService(estado);

                    var venues = _service.Get();

                    if (venues.Count > 40)
                    {
                        var qtdeHotel = _service.GetHotels().Count();
                        if (qtdeHotel == 0 || (venues.Count() - qtdeHotel) > 180)
                        {
                            _local = estado + ", USA";
                            _hotelBasic = new List<VenueList>();
                            _hotelBasic.AddRange(venues);
                            _hotelBasic.RemoveRange(0, qtdeHotel);
                            //// Console.WriteLine("---------------------------------------------------------------");
                            // Console.WriteLine(_hotelBasic.Count() + " Hoteis from " + estado);
                            GetHotelFullInfo();
                        }
                        continue;
                    }

                    _local = estado + ", USA";
                    // Console.WriteLine("Starting basic info from" + estado);
                    GetHotelIds();

                    var hotelBasicInfo = _service.Get();
                    _hotelBasic = new List<VenueList>();
                    _hotelBasic.AddRange(hotelBasicInfo);
                    // Console.WriteLine("---------------------------------------------------------------");
                    // Console.WriteLine(_hotelBasic.Count() + " Hoteis from " + estado);
                    GetHotelFullInfo();
                    //catch (Exception ex)
                    //{
                    //    Log log = new Log { IdObjeto = estado, Exception = ex.InnerException.ToString(), Metodo = "Escopo geral", TipoObjeto = estado  };
                    //    _service.CreateLog(log);

                    //}
                }
            }
            if (local == "e")
            {
                var paises = Helper.InitializeEuList();
                foreach (var pais in paises)
                {
                    _service = new HotelService(pais);
                    var venues = _service.Get();
                    if (venues.Count > 40)
                    {
                        var qtdeHotel = _service.GetHotels().Count();
                        //   if (qtdeHotel == 0 || (venues.Count() - qtdeHotel) > 180)
                        // {
                        _local = pais;
                        _hotelBasic = new List<VenueList>();
                        _hotelBasic.AddRange(venues);
                        _hotelBasic.RemoveRange(0, qtdeHotel);
                        // Console.WriteLine("---------------------------------------------------------------");
                        // Console.WriteLine(_hotelBasic.Count() + " Hoteis from " + pais);
                        GetHotelFullInfo();
                        // }
                        continue;
                    }

                    _local = pais;
                    // Console.WriteLine("Starting basic info from" + pais);
                    GetHotelIds();

                    var hotelBasicInfo = _service.Get();
                    _hotelBasic = new List<VenueList>();
                    _hotelBasic.AddRange(hotelBasicInfo);
                    // Console.WriteLine("---------------------------------------------------------------");
                    // Console.WriteLine(hotelBasicInfo.Count() + " Hoteis from " + pais);
                    GetHotelFullInfo();

                }
            }

        }

        public void UpdateBrazilianPlaces()
        {
            _service = new HotelService("Brasil");
            var hotelBasicInfo = _service.Get();
            _hotelBasic = new List<VenueList>();
            _hotelBasic.AddRange(hotelBasicInfo.Take(5));
            // Console.WriteLineAdd
            // Console.WriteLine(hotelBasicInfo.Count() + " Hoteis from " + pais);
            GetHotelFullInfo();
           // return _hotelBasic;
        }

        public async Task<string> UpdateByCountry(string pais, int Take)
        {
            _service = new HotelService(pais);
            var hotelBasicInfo = _service.Get();
            _hotelBasic = new List<VenueList>();
            _hotelBasic.AddRange(hotelBasicInfo.Take(Take));
            
            foreach(var _hotel in _hotelBasic)
            {
                Hotel hotelData = GetRemoteHotelInfo(_hotel.id);


                if(hotelData.basicProfile.venueDescription != null) {

                    List<string> listDescription = new List<string>()
                            {
                                hotelData.basicProfile.venueDescription
                            };

                    var portugueseDescription = await TranslateDescription(listDescription, "Portuguese");                   

                    var spanishDescription = await TranslateDescription(listDescription, "Spanish");
                    hotelData.basicProfile.spanishDescription = spanishDescription.First();
                    hotelData.basicProfile.portugueseDescription = portugueseDescription.First();
                    hotelData.IsUpdated = true; 

                    if (_service.UpdateHotel(hotelData.ofrgId, hotelData) != null) { }
                    // Console.WriteLine("Added Full info of: " + hotel.basicProfile.name);
                    else { }
                        // Console.WriteLine("An error occurred on " + hotel.basicProfile.name + " see details on log");
                    }
            }

            return pais; 
        }

        private Hotel GetRemoteHotelInfo(string id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = $"https://www.cvent.com/api/venue-profile/v1/venue/{id}/fullProfile?env=P2";

                HttpResponseMessage response = null;
                //string json = JsonConvert.SerializeObject(obj);
                response = client.GetAsync(url).Result;

                var result = response.Content.ReadAsStringAsync().Result;
                var hotel = JsonConvert.DeserializeObject<Hotel>(result);

                return hotel;             
            }
        }
        private static void GetHotelIds(int startIndex = 0)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "https://www.cvent.com/api/csn-search/csn-search/v1/search/venues?env=P2";

                HttpResponseMessage response = null;
                try
                {
                    var content = BasicInfoJson(startIndex);
                    response = client.PostAsync(url, content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<Search>(result);
                    foreach (var venue in json.searchResult.venueList)
                    {
                        if (_service.Create(venue) != null) { }
                        // Console.WriteLine("Added basic info of: " + venue.name);
                        else { }
                            // Console.WriteLine("An error occurred on " + venue.name + " see details on log");
                    }
                    while (json.searchResult.size == 25)
                    {
                        GetHotelIds(startIndex += 25);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _service.CreateLog(new Log { IdObjeto = "", Exception = ex.Message, Metodo = "get ids", TipoObjeto = "Venues" });
                }

            }
        }

        private async static void GetHotelFullInfo()
        {
           foreach(var item in _hotelBasic)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = $"https://www.cvent.com/api/venue-profile/v1/venue/{item.id}/fullProfile?env=P2";

                    HttpResponseMessage response = null;
                    //string json = JsonConvert.SerializeObject(obj);
                    response = client.GetAsync(url).Result;

                    var result = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var hotel = JsonConvert.DeserializeObject<Hotel>(result);
                        if (hotel.ofrgId != null)
                        {
                            List<string> listDescription = new List<string>()
                            {
                                hotel.basicProfile.venueDescription
                            }; 

                            var portuguese_desc = await TranslateDescription(listDescription, "Portuguese");

                            if (portuguese_desc != null)
                                hotel.basicProfile.portugueseDescription = portuguese_desc.First();
                            
                            var spanish_desc = await TranslateDescription(listDescription, "Spanish");

                            if (spanish_desc != null)
                                hotel.basicProfile.spanishDescription = spanish_desc.First();


                            if (_service.CreateHotel(hotel) != null) { }
                            // Console.WriteLine("Added Full info of: " + hotel.basicProfile.name);
                            else { }
                                // Console.WriteLine("An error occurred on " + hotel.basicProfile.name + " see details on log");
                        }
                        else
                            _service.CreateLog(new Log { IdObjeto = item.id, Exception = "Doesnt exists a hotel with this id", Metodo = "get full info", TipoObjeto = "Hotel" });
                    }
                    catch (Exception ex)
                    {
                        _service.CreateLog(new Log { IdObjeto = item.id, Exception = ex.Message, Metodo = "Deserialize Hotel", TipoObjeto = "Hotel" });
                    }

                }
                Thread.Sleep(1500);
            }
        }

        private static Coords GetStatesCordenadas()
        {
            var body = "{\"queryString\":\"" + _local + "\",\"presetLocations\":[],\"language\":\"EN\",\"region\":\"US\",\"autosuggestion\":null,\"useAlternateEnvironment\":false}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "https://www.cvent.com/api/csn-search/csn-search/v1/parseQuery?env=P2";

                HttpResponseMessage response = null;
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                response = client.PostAsync(url, content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<Coords>(result);
                return obj;
            }
        }

        private static Coords GetCountriesCordenadas()
        {
            var body = "{\"queryString\":null,\"language\":\"EN\",\"region\":\"US\",\"autosuggestion\":{\"queryString\":\"" + _local + "\",\"provider\":\"google\",\"types\":[\"country\",\"political\",\"geocode\"],\"id\":\"\"},\"useAlternateEnvironment\":false}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "https://www.cvent.com/api/csn-search/csn-search/v1/parseQuery?env=P2";

                HttpResponseMessage response = null;
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                response = client.PostAsync(url, content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<Coords>(result);
                return obj;
            }
        }

        private static StringContent BasicInfoJson(int startIndex)
        {
            if (_local.Contains("Ireland"))
                return new StringContent("{\"filters\":[{\"type\":\"VENUE_TYPE\",\"values\":[\"BOUTIQUE_HOTEL\",\"CONFERENCE_CENTER\",\"EXPOSITION_CENTER\",\"HOTEL\",\"LUXURY_HOTEL\",\"RESORT\",\"CONVENTION_CENTER\",\"CVB\",\"SPECIAL_EVENT_VENUE\"],\"occurrenceType\":\"SHOULD\"},{\"type\":\"BOUNDING_BOX\",\"boundingBox\":{\"northEastPoint\":{\"lat\":55.38294149999999,\"lon\":-5.431909999999999},\"southWestPoint\":{\"lat\":51.4475448,\"lon\":-10.4800237}},\"geographicPoint\":{\"lat\":53.1423672,\"lon\":-7.692053599999999}}],\"sort\":{\"type\":\"CVENT_PICKS\",\"boundingBox\":{\"northEastPoint\":{\"lat\":55.38294149999999,\"lon\":-5.431909999999999},\"southWestPoint\":{\"lat\":51.4475448,\"lon\":-10.4800237}}},\"includeRanges\":true,\"from\":" + startIndex + ",\"size\":25,\"view\":\"LIST\",\"useAlternateEnvironment\":false}", Encoding.UTF8, "application/json");
            Coords cords;
            if (_local.Contains(", USA"))
                cords = GetStatesCordenadas();
            else
                cords = GetCountriesCordenadas();

            var boudingNorthEast = cords.boundingBoxFilter.boundingBox.northEastPoint;
            var boudingSouthWest = cords.boundingBoxFilter.boundingBox.southWestPoint;
            var geographicPoint = cords.geographicPointFilter.geographicPoint;
            var body = "{\"filters\":[{\"type\":\"VENUE_TYPE\",\"values\":[\"BOUTIQUE_HOTEL\",\"CONFERENCE_CENTER\",\"EXPOSITION_CENTER\",\"HOTEL\",\"LUXURY_HOTEL\",\"RESORT\",\"CONVENTION_CENTER\",\"CVB\",\"SPECIAL_EVENT_VENUE\"]" +
                ",\"occurrenceType\":\"SHOULD\"},{\"type\":\"POLYGON\",\"values\":[\"" + cords.additionalFilters[0].values[0] + "\"],\"geographicPoint\":{\"lat\":" + geographicPoint.lat + ",\"lon\":" + geographicPoint.lon + "}}],\"sort\":{\"type\":\"CVENT_PICKS\",\"boundingBox\":" +
                "{\"northEastPoint\":{\"lat\":" + boudingNorthEast.lat + ",\"lon\":" + boudingNorthEast.lon + "},\"southWestPoint\":{\"lat\":" + boudingSouthWest.lat + ",\"lon\":" + boudingSouthWest.lon + "}}},\"includeRanges\":true,\"from\":" + startIndex + ",\"size\":25,\"view\":\"LIST\",\"useAlternateEnvironment\":false}";
            var teste = new StringContent(body, Encoding.UTF8, "application/json");

            return teste;

        }
        
        public void GenerateJsonPlaces()
        {
            FullInfoRepository repository = new FullInfoRepository();
            var collections = repository.GetAllCollections();

            foreach(dynamic collection in collections)
            {
                var name = collection["name"].ToString(); 
                if (name.Contains("Hotel"))
                {
                    var country = name.Replace("Hotel", ""); 
                    _service = new HotelService(country);

                    var cityList = _service.GetCities();
                    var stateList = _service.GetStates();
                    var countryList = _service.GetCountry();

                    foreach(var city in cityList)
                    {
                        var local = new
                        {
                            cidade = city                           

                        };
                    }
                }
            }
        }
      
        public static void adjustLanguage(ref string language)
        {
            language = language.Split('-')[0];
        }
      
        public async static Task<IList<string>> TranslateDescription(IList<string> list, string isoLanguageTo)
        {
            string description = string.Join("|", list);

            var translate = new GoogleTranslator();
            Language fromLang = Language.Auto;
            Language from = GoogleTranslator.GetLanguageByISO(fromLang.FullName);
            Language to = GoogleTranslator.GetLanguageByName(isoLanguageTo);
            TranslationResult result = await translate.TranslateLiteAsync(description, fromLang, to);
            //The result is separated by the suggestions and the '\n' symbols
            string[] resultSeparated = result.FragmentedTranslation;

            //You can get all text using MergedTranslation property
            string resultMerged = result.MergedTranslation;

            //There is also original text transcription
            string transcription = result.TranslatedTextTranscription; // Kon'nichiwa! Ogenkidesuka?
            return resultMerged.Split('|').Select(x => x.TrimStart()).ToList();
        }
    }
}
