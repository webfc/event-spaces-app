using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Crawler.Model
{
    public class Search
    {
        public SearchResult searchResult { get; set; }


        public class SearchResult
        {
            public List<VenueList> venueList { get; set; }
            public int size { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class VenueList
        {
            [BsonId]
            public string id { get; set; }
            public string name { get; set; }
            public AirportDistance airportDistance { get; set; }
            public List<string> amenities { get; set; }
            public string brandName { get; set; }
            public string chainName { get; set; }
            public List<object> visibleAffiliationNames { get; set; }
            public string city { get; set; }
            public string mainAddress { get; set; }
            public string secondaryAddress { get; set; }
            public string postalCode { get; set; }
            public string countryCode { get; set; }
            public string displayOpeningDateUntil { get; set; }
            public DistanceFromSearchLocation distanceFromSearchLocation { get; set; }
            public HeroImage heroImage { get; set; }
            public LargestCeilingHeight largestCeilingHeight { get; set; }
            public LargestMeetingSpace largestMeetingSpace { get; set; }
            public string listingText { get; set; }
            public string adTagline { get; set; }
            public GeographicLocation geographicLocation { get; set; }
            public MainImage mainImage { get; set; }
            public string metroAreaName { get; set; }
            public string openingDate { get; set; }
            public string preferredRating { get; set; }
            public SecondLargestMeetingSpace secondLargestMeetingSpace { get; set; }
            public string stateProvinceCode { get; set; }
            public List<string> tags { get; set; }
            public int totalMeetingRoom { get; set; }
            public TotalRoomSpaceArea totalRoomSpaceArea { get; set; }
            public double totalRoomSpaceAreaMetricScorable { get; set; }
            public int totalSleepingRoom { get; set; }
            public string type { get; set; }
            public string code { get; set; }
            public int yearBuild { get; set; }
            public int yearRenovate { get; set; }
            public List<string> subRegions { get; set; }
            public List<Promotion> promotions { get; set; }
            public string diamondLevel { get; set; }
            public int numberOfHotels { get; set; }
            public int numberOfRestaurants { get; set; }
            public int specialEventVenuesNumber { get; set; }
            public int numberOfCommittableSleepingRooms { get; set; }
            public bool displaySearchResultImage { get; set; }
            public bool displayHeroImage { get; set; }
            public bool needDateFlag { get; set; }
            public bool promotionsAllowed { get; set; }
        }

        public class AirportDistance
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class DistanceFromSearchLocation
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class HeroImage
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class LargestCeilingHeight
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class LargestMeetingSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class GeographicLocation
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public class MainImage
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class SecondLargestMeetingSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class TotalRoomSpaceArea
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class PromotionDate
        {
            public string promotionStartDate { get; set; }
            public string promotionEndDate { get; set; }
        }

        public class Promotion
        {
            public string promotionId { get; set; }
            public string activeStartDate { get; set; }
            public string activeEndDate { get; set; }
            public string title { get; set; }
            public string listingText { get; set; }
            public string promoType { get; set; }
            public string discountType { get; set; }
            public string discountCurrency { get; set; }
            public double discountAmountMin { get; set; }
            public double discountAmountMax { get; set; }
            public List<string> validDayOfWeek { get; set; }
            public int displayOrder { get; set; }
            public List<PromotionDate> promotionDates { get; set; }
        }
    }
}
