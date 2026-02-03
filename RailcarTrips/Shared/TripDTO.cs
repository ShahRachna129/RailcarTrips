using System;
namespace Shared
{
	public class TripDTO
	{
        //public int Id { get; set; }
        //public string? EquipmentId { get; set; } = "";

        //public int? OriginCityId { get; set; }
        //public CityDTO? OriginCity { get; set; }

        //public int? DestinationCityId { get; set; }
        //public CityDTO? DestinationCity { get; set; }

        //public DateTime StartUtc { get; set; }
        //public DateTime? EndUtc { get; set; }

        //public double TotalTripHours { get; set; }
        //public List<EquipmentEventDTO> Events { get; set; } = new();
        public int TripID { get; set; }
        public string Equipment_Id { get; set; } = "";

        public int OriginCityId { get; set; }
        //public City OriginCity { get; set; }
        public string OriginCity { get; set; } = "";
        public int? DestinationCityId { get; set; }
        //public City DestinationCity { get; set; }
        public string DestinationCity { get; set; } = "";
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public double TotalTripHours { get; set; }

        public List<EventDTO> Events { get; set; } = new();

    }
}

