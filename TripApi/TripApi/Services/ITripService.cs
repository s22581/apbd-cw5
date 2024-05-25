namespace TripApi.Services;

using TripApi.Models.DTOs;
using TripApi.Repositories;
public interface ITripService
{
    List<GetTripDto> GetSortedTrips();
    void AssignClientToTrip(int idTrip, AssignClientToTripDto dto);
}