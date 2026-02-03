using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server;   // EF model
using Server.Models;
using Server.Services;
using Shared;          // DTO


namespace Server.Controllers	
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController:ControllerBase
	{
		private readonly RailcarDbContext _db;
        private readonly TripProcessor _processor;
        public TripsController(RailcarDbContext db, TripProcessor processor)
		{
			_db = db;
            _processor = processor;
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadCsv(IFormFile file)
        //{
        //    try
        //    {
        //        //var maxFileSize = 5 * 1024 * 1024; // 5 MB
        //        if (file == null || file.Length == 0)
        //            return BadRequest("No file uploaded.");

        //        var trips = await _processor.ProcessEventsAsync(file.OpenReadStream());
        //        return Ok(trips);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

     
        
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                    var trips = await
                    (from t in _db.Trips
                     join o in _db.canadian_cities
                         on t.OriginCityId equals o.City_Id
                     join d in _db.canadian_cities
                         on t.DestinationCityId equals d.City_Id
                     select new TripDTO
                     {
                         TripID = t.TripID,
                         Equipment_Id = t.Equipment_Id,
                         OriginCity = o.City_Name,
                         DestinationCity = d.City_Name,
                         StartUtc = t.StartUtc,
                         EndUtc = t.EndUtc,
                         TotalTripHours = t.TotalTripHours
                     }).OrderBy(t=>t.Equipment_Id).ThenBy(t=>t.StartUtc)
                    .ToListAsync();

                        return Ok(trips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private static int? GetDestinationCityId(int? id)
        {
            if (id == null)
            { return 0; }
            else { return (int)id; }
        }

        [HttpGet("{tripId}/events")]
        public async Task<IActionResult> GetTripEvents(int tripId)
        {
            try
            {
                var events = await _db.Events
                 .Select(t => new EventDTO
                 {
                    Id = t.Id,
                    EquipmentId = t.EquipmentId,
                    EventCode=t.EventCode,
                    CityId=t.CityId,
                    EventLocalTime=t.EventLocalTime,
                    EventUtcTime=t.EventUtcTime
                 }).Where(e => e.TripId == tripId)
                .OrderBy(e => e.EventUtcTime)
                 .ToListAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
     
        [HttpGet("/api/cities")]
        public async Task<List<CityDTO>> GetCities()
        {
            return await _db.canadian_cities.Select(t => new CityDTO
            {
                Id = t.City_Id,
               Name=t.City_Name,
               TimeZoneId=t.Time_Zone
            }).ToListAsync();
        }
    }
}

