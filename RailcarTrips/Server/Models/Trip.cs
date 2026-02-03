using System;
namespace Server.Models
{
	public class Trip
	{
        public int TripID { get; set; }
        public string Equipment_Id { get; set; }

        public int OriginCityId { get; set; }
        //public City OriginCity { get; set; }

        public int? DestinationCityId { get; set; }
        //public City DestinationCity { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public double TotalTripHours { get; set; }

        public List<Event> Events { get; set; } = new();


      
    }
}

