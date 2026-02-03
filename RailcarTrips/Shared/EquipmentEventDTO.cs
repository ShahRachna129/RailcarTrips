using System;
namespace Shared
{
	public class EquipmentEventDTO
	{
		
		public int Id { get; set; }
		public string EquipmentId { get; set; } = "";
		public string EventCode { get; set; } = "";
		public int CityId { get; set; }
		public CityDTO? City { get; set; }
		public DateTime LocalEventTime { get; set; }
		public DateTime UtcEventTime { get; set; }
		public int? TripId { get; set; }
		public TripDTO? Trip { get; set; }
	}
}

