using System;
namespace Server.Models
{
	public class Event
	{
        public int Id { get; set; }

        public string? EquipmentId { get; set; }

        public string? EventCode { get; set; }   // W or Z
        public int EventOrder { get; set; }

        public int CityId { get; set; }

        public DateTime EventLocalTime { get; set; }

        public DateTime EventUtcTime { get; set; }

        public int? TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}

