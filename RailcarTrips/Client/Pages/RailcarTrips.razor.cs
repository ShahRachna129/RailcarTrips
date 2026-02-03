using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Shared;


namespace Client.Pages
{
	public partial class RailcarTrips : ComponentBase
    {   
        private List<TripDTO>? Trips;
        private List<EventDTO>? SelectedTripEvents;
        private List<CityDTO>? Cities;

        protected override async Task OnInitializedAsync()
        {
            Trips = await Http.GetFromJsonAsync<List<TripDTO>>("api/trips");
        }

        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream());
                content.Add(fileContent, "file", file.Name);

                var response = await Http.PostAsync("api/trips/upload", content);
                if (response.IsSuccessStatusCode)
                {
                    Trips = await Http.GetFromJsonAsync<List<TripDTO>>("api/trips");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Upload failed: {error}");
                }
            }
        }

        private string GetCityName(int? cityId) => Cities?.FirstOrDefault(c => c.Id == cityId)?.Name ?? "";

        private async Task ShowEvents(int tripId)
        {
            SelectedTripEvents = await Http.GetFromJsonAsync<List<EventDTO>>($"api/trips/{tripId}/events");
        }
    }
}

