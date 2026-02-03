using System;
namespace Server.Models
{
	public class canadian_cities
	{
       public int City_Id { get; set; }
        public string City_Name { get; set; } = "";
        public string Time_Zone { get; set; } = "";
        public int TimeZoneHrs { get; set; }
     public int  TimeZoneMinutes { get; set; }
    }
}

