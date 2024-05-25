using Microsoft.EntityFrameworkCore;
using TripApi.Models;


namespace TripApi.Repositories;

    public class TripRepository : ITripRepository
    {
        private readonly TripDbContext _context;

        public TripRepository(TripDbContext context)
        {
            _context = context;
        }

        public void AddTripToClientTrips(ClientTrip clientTrip)
        {
            _context.ClientTrips.Add(clientTrip);
            _context.SaveChanges();
        }

        public Trip GetTripById(int idTrip)
        {
            return _context.Trips.FirstOrDefault(t => t.IdTrip == idTrip);
        }

        public IEnumerable<Trip> GetTrips()
        {
            return _context.Trips
                .Include(t => t.ClientTrips)
                .ThenInclude(ct => ct.IdClientNavigation)
                .Include(t => t.IdCountries);
        }
    }
