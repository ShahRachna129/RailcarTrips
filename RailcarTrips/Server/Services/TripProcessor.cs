using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Microsoft.AspNetCore.Components.Forms;
using Shared;
using static System.Net.WebRequestMethods;
using System.Numerics;

namespace Server.Services
{
    public class TripProcessor
    {
        private readonly RailcarDbContext _db;
        public TripProcessor(RailcarDbContext db)
        {
            _db = db;
        }
        //1) Imported Table into database
        //2) First I started with C# Code but endup with the SQL Procedure (ProcessTrip_Procedure.sq)
       //Need to write code for call Procedure for TripProcess I run that manually on SQL 


        //public async Task<List<Trip>> ProcessEventsAsync(Stream csvStream)
        //{
        //    using var reader = new StreamReader(csvStream);
        //    var events = new List<Event>();
        //    var header = await reader.ReadLineAsync();//skip header


        //    while (!reader.EndOfStream)
        //    {
        //        var line = await reader.ReadLineAsync();
        //        var parts = line.Split(',');
        //        var equipmentId = parts[0];
        //        var eventcode = parts[1];
        //        var eventTimeLocal = DateTime.Parse(parts[2], CultureInfo.InvariantCulture);
        //        int city_id = Convert.ToInt32(parts[3]);
        //        var eventorder = Convert.ToInt32(parts[4]);
        //        var cityName = await _db.canadian_cities.Where(ct => ct.City_Id == city_id).Select(c => c.City_Name).FirstOrDefaultAsync();
        //        var city = await _db.canadian_cities.FirstOrDefaultAsync(c => c.City_Name == cityName);
        //        if (city == null)
        //        {
        //            throw new Exception($"City not found in database: {city_id}");
        //        }
        //        var tz = TimeZoneInfo.FindSystemTimeZoneById(city.Time_Zone);

        //        DateTime eventTimeUtc;

        //        if (tz.IsInvalidTime(eventTimeLocal))
        //        {
        //            eventTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
        //                eventTimeLocal.AddHours(1), tz);
        //        }
        //        else if (tz.IsAmbiguousTime(eventTimeLocal))
        //        {
        //            var offsets = tz.GetAmbiguousTimeOffsets(eventTimeLocal);
        //            eventTimeUtc = DateTime.SpecifyKind(
        //                eventTimeLocal - offsets[0], DateTimeKind.Utc);
        //        }
        //        else
        //        {
        //            eventTimeUtc = TimeZoneInfo.ConvertTimeToUtc(eventTimeLocal, tz);
        //        }

        //        events.Add(new Event
        //        {
        //            EquipmentId = equipmentId,
        //            CityId = city.Id,
        //            EventCode = eventcode,
        //            EventLocalTime = eventTimeLocal,
        //            EventUtcTime = eventTimeUtc,
        //            EventOrder=eventorder
        //        });

        //    }
        //    //var groupedEvents = events
        //    //.Where(e => e.EventCode == "W" || e.EventCode == "Z")
        //    //.GroupBy(e => e.EquipmentId).OrderBy(g => g.Key); 
        //    var groupedEvents = events.GroupBy(e => e.EquipmentId).OrderBy(g => g.Key);

        //    var trips = new List<Trip>();
        //    foreach (var eqGroup in groupedEvents)
        //    {
        //        Trip? currentTrip = null;
        //        var orderedEvents = eqGroup.OrderBy(e => e.EventUtcTime).ThenBy(e => e.EventOrder).ToList();
        //        foreach (var ev in orderedEvents)
        //        {

        //            if (ev.EventCode == "W")
        //            {
        //                currentTrip = new Trip
        //                {
        //                    Equipment_Id = ev.EquipmentId,
        //                    OriginCityId = ev.CityId,

        //                    StartUtc = ev.EventUtcTime,
        //                    Events = new List<Event> { ev }
        //                };

        //                trips.Add(currentTrip);
        //            }

        //            else if (ev.EventCode == "Z" && currentTrip != null)
        //            {
        //                //// Ignore bad city id
        //                //if (ev.CityId <= 0)
        //                //    continue;

        //                currentTrip.DestinationCityId = ev.CityId;
        //                currentTrip.EndUtc = ev.EventUtcTime;
        //                currentTrip.TotalTripHours = ( currentTrip.StartUtc- currentTrip.EndUtc).TotalHours;
        //                ev.Trip = currentTrip;

        //                currentTrip.Events.Add(ev);

        //                trips.Add(currentTrip);
        //                currentTrip = null;
        //            }
        //            else { currentTrip?.Events.Add(ev); }

        //        }
        //    }
        //    trips = trips
        //.Where(t => t.DestinationCityId > 0)
        //.ToList();
        //  _db.Trips.AddRange(trips);
        //    await _db.SaveChangesAsync();
        //    return trips;

        //}

    }

}