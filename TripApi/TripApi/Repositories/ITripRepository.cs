namespace TripApi.Repositories;

using TripApi.Models;
public interface ITripRepository
{
    IEnumerable<Trip> GetTrips();
    Trip GetTripById(int idTrip);
    void AddTripToClientTrips(ClientTrip clientTrip);
}