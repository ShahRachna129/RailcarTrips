using System;
namespace Server.Models
{
	public class EquipmentEvent
	{
		
		public int Id { get; set; }
		public string EquipmentId { get; set; } = "";
		public string EventCode { get; set; } = "";
		public int CityId { get; set; }
		public canadian_cities City { get; set; }
		public DateTime LocalEventTime { get; set; }
		public DateTime UtcEventTime { get; set; }
		public int? TripId { get; set; }
		public Trip? Trip { get; set; }
	}
}

