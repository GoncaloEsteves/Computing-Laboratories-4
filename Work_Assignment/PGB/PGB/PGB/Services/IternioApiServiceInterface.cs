using System.Threading.Tasks;
using PGB.Models;

namespace PGB.Services
{
    public interface IternioApiServiceInterface
    {
        Task<TripPlan> GetTripPlan(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude,
            string typecode, int initialSoc, int refConsumption, bool avoidTolls, bool avoidHighways, bool avoidFerries);
    }
}