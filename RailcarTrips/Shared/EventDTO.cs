using System;
namespace Shared
{
	public class EventDTO
    {
        public int Id { get; set; }

        public string? EquipmentId { get; set; }

        public string? EventCode { get; set; }   // W or Z

        public int CityId { get; set; }

        public DateTime EventLocalTime { get; set; }

        public DateTime EventUtcTime { get; set; }

        public int? TripId { get; set; }
        public TripDTO? Trip { get; set; }
    }
}

