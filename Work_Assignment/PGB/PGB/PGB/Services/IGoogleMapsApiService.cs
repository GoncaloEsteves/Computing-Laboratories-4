using System.Collections.Generic;
using System.Threading.Tasks;
using PGB.Models;

namespace PGB.Services
{
    public interface IGoogleMapsApiService
    {
        // Invoke method to obtain the polylines by the given waypoints (steps) that the route needs to pass
        Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, 
            string destinationLongitude, List<IternioStep> steps);
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}