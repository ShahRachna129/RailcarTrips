using System;
namespace Server.Models
{
	public class Event_Code_Definations
	{
		public int Id { get; set; }
		public string Event_code { get; set; } = "";
		public string Event_description { get; set; } = "";
		public string Long_description { get; set; } = "";
		public int EventCodeDefiaiton_Order { get; set; }
		
	}
}

