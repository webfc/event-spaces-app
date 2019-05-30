using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Core.Model
{
    public class HotelFullInfo
    {
        [BsonId]
        public string ofrgId { get; set; }
        public BasicProfile basicProfile { get; set; }
        public Highlights highlights { get; set; }
        public List<Amenity> amenities { get; set; }
        public List<MeetingRoom> meetingRooms { get; set; }
        public List<Rating> ratings { get; set; }
        public List<Image> images { get; set; }
        public List<AdditionalDocument> additionalDocuments { get; set; }
        public GuestRoom guestRoom { get; set; }
        public AreaInformation areaInformation { get; set; }
        public MeetingSpace meetingSpace { get; set; }
        public List<ParkingRate> parkingRates { get; set; }
        public List<Promotion> promotions { get; set; }
        public List<Attraction> attractions { get; set; }
        public List<ClosestAirport> closestAirports { get; set; }
        public string eventInsurance { get; set; }
        public RestaurantInformation restaurantInformation { get; set; }
        public List<NeedDate> needDates { get; set; }

        public class OfrgImage
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class AdditionalDocument
        {
            public string id { get; set; }
            public string name { get; set; }
            public int typeId { get; set; }
            public string typeName { get; set; }
            public string documentCategory { get; set; }
            public string path { get; set; }
        }

        public class HeroImage
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class NeedDate
        {
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
        }

        public class BasicProfile
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string stateProvinceCode { get; set; }
            public string countryCode { get; set; }
            public string postalCode { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string currencyCode { get; set; }
            public string diamondLevel { get; set; }
            public bool hasPromotions { get; set; }
            public string venueTypeId { get; set; }
            public int venueMinCapacity { get; set; }
            public OfrgImage ofrgImage { get; set; }
            public bool greenFriendly { get; set; }
            public string venueAwards { get; set; }
            public string profileUrl { get; set; }
            public string metroAreaName { get; set; }
            public string listingText { get; set; }
            public string venueDescription { get; set; }
            public string venueDescriptionTitle { get; set; }
            public string offeringStatus { get; set; }
            public int listTypeId { get; set; }
            public double largestMeetingRoom { get; set; }
            public double secondLargestMeetingRoom { get; set; }
            public string measurementUnit { get; set; }
            public int measurementUnitId { get; set; }
            public string additionalInformation { get; set; }
            public string cancellationPolicy { get; set; }
            public string facilityRestrictions { get; set; }
            public HeroImage heroImage { get; set; }
            public string gettingHere { get; set; }
            public string dmaiUrl { get; set; }
            public string twitterUrl { get; set; }
            public string facebookUrl { get; set; }
            public string virtualTourUrl { get; set; }
            public string generalImageId { get; set; }
            public string heroImageId { get; set; }
            public bool profileNearbyAdsFlag { get; set; }
            public bool profilePeopleAlsoViewedFlag { get; set; }
        }

        public class TotalMeetingSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class Highlights
        {
            public string chainName { get; set; }
            public string brandName { get; set; }
            public List<object> affiliations { get; set; }
            public int totalSleepingRoom { get; set; }
            public int totalMeetingRoom { get; set; }
            public string openingDate { get; set; }
            public bool displayOpeningDate { get; set; }
            public int buildYear { get; set; }
            public double priceCategory { get; set; }
            public int renovationYear { get; set; }
            public TotalMeetingSpace totalMeetingSpace { get; set; }
            public int maxCapacityStanding { get; set; }
            public int maxCapacitySeated { get; set; }
        }

        public class Amenity
        {
            public string amenityType { get; set; }
            public List<string> amenityNameList { get; set; }
            public string typeAmenity { get; set; }
            public List<string> amenityList { get; set; }
        }

        public class TotalSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class CeilingHeight
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class RoomLength
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class RoomWidth
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class RoomImageVariant
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class SeatingCapacity
        {
            public int typeId { get; set; }
            public string type { get; set; }
            public int capacity { get; set; }
        }

        public class ExhibitCapacity
        {
            public int typeId { get; set; }
            public string type { get; set; }
            public int capacity { get; set; }
        }

        public class Amenity2
        {
            public string roomAmenityId { get; set; }
            public string roomAmenityName { get; set; }
            public int roomAmenityTypeId { get; set; }
            public int roomAmenityDisplayOrder { get; set; }
        }

        public class MeetingRoom
        {
            public string id { get; set; }
            public string name { get; set; }
            public TotalSpace totalSpace { get; set; }
            public bool hasOutdoorSpaceAmenity { get; set; }
            public CeilingHeight ceilingHeight { get; set; }
            public RoomLength roomLength { get; set; }
            public RoomWidth roomWidth { get; set; }
            public string roomImagePath { get; set; }
            public RoomImageVariant roomImageVariant { get; set; }
            public int displayOrder { get; set; }
            public int maxCapacity { get; set; }
            public List<SeatingCapacity> seatingCapacities { get; set; }
            public List<ExhibitCapacity> exhibitCapacities { get; set; }
            public List<Amenity2> amenities { get; set; }
            public double moveInRate { get; set; }
            public double moveOutRate { get; set; }
            public double eventRate { get; set; }
            public string roomDescription { get; set; }
        }

        public class Rating
        {
            public int agencyId { get; set; }
            public bool primaryRatingFlag { get; set; }
            public string agencyName { get; set; }
            public string agencyDesc { get; set; }
            public string ratingName { get; set; }
            public string ratingDesc { get; set; }
        }

        public class ImageURL
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class Image
        {
            public string id { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string description { get; set; }
            public string group { get; set; }
            public ImageURL imageURL { get; set; }
            public string providerType { get; set; }
            public int selectionWidth { get; set; }
            public int selectionHeight { get; set; }
            public int selectionTopLeftX { get; set; }
            public int selectionTopLeftY { get; set; }
        }

        public class RoomDetail
        {
            public string type { get; set; }
            public int count { get; set; }
            public double minRate { get; set; }
            public double maxRate { get; set; }
        }

        public class GuestRoom
        {
            public int totalRoomCount { get; set; }
            public double taxRate { get; set; }
            public double occupancyRate { get; set; }
            public List<RoomDetail> roomDetails { get; set; }
        }

        public class ConventionCenterSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class AreaInformation
        {
            public int hotelsNumber { get; set; }
            public int specialEventVenuesNumber { get; set; }
            public int restaurantsNumber { get; set; }
            public ConventionCenterSpace conventionCenterSpace { get; set; }
            public int sleepingRoomsNumber { get; set; }
            public int largestHotel { get; set; }
            public double averageHotelRoomRate { get; set; }
            public double averageDailyFoodCost { get; set; }
            public double taxRate { get; set; }
            public double occupancyRate { get; set; }
        }

        public class MeetingSpace2
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class LargestMeetingRoomSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class SecondLargestMeetingRoomSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class PrivateSpace
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class MeetingSpace
        {
            public MeetingSpace2 meetingSpace { get; set; }
            public int numberOfMeetingRooms { get; set; }
            public LargestMeetingRoomSpace largestMeetingRoomSpace { get; set; }
            public SecondLargestMeetingRoomSpace secondLargestMeetingRoomSpace { get; set; }
            public PrivateSpace privateSpace { get; set; }
        }

        public class ParkingRate
        {
            public string type { get; set; }
            public double rate { get; set; }
        }

        public class ImageFilePath
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class PromotionDate
        {
            public string startDate { get; set; }
            public string endDate { get; set; }
        }

        public class PromotionDetails
        {
            public string description { get; set; }
            public string additionalInformation { get; set; }
            public List<string> daysOfWeek { get; set; }
            public List<object> attachments { get; set; }
        }

        public class Promotion
        {
            public string id { get; set; }
            public string name { get; set; }
            public ImageFilePath imageFilePath { get; set; }
            public string type { get; set; }
            public double minRate { get; set; }
            public double maxRate { get; set; }
            public string currency { get; set; }
            public int displayOrder { get; set; }
            public List<PromotionDate> promotionDates { get; set; }
            public PromotionDetails promotionDetails { get; set; }
            public string promotionListType { get; set; }
        }

        public class ImageUrl2
        {
            public string original { get; set; }
            public string large { get; set; }
            public string medium { get; set; }
            public string small { get; set; }
            public string extraSmall { get; set; }
        }

        public class Attraction
        {
            public string name { get; set; }
            public string type { get; set; }
            public int distance { get; set; }
            public string distanceType { get; set; }
            public string address1 { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string postalCode { get; set; }
            public ImageUrl2 imageUrl { get; set; }
            public int displayOrder { get; set; }
            public string description { get; set; }
            public string url { get; set; }
        }

        public class AirportDistance
        {
            public double imperialValue { get; set; }
            public double metricValue { get; set; }
        }

        public class ClosestAirport
        {
            public AirportDistance airportDistance { get; set; }
            public bool isVenueDefault { get; set; }
        }

        public class RestaurantInformation
        {
            public string executiveChef { get; set; }
            public List<object> operatingHours { get; set; }
        }
    }
}
