using Microsoft.EntityFrameworkCore;
using TripApi.Models;

namespace TripApi.Repositories;


    public class ClientRepository : IClientRepository
    {
        private readonly TripDbContext _context;

        public ClientRepository(TripDbContext context)
        {
            _context = context;
        }

        public Client GetClientById(int idClient)
        {
            return _context.Clients.Include(c => c.ClientTrips)
                .FirstOrDefault(c => c.IdClient == idClient);
        }

        public Client GetClientByPeselNumber(string pesel)
        {
            return _context.Clients.Include(c => c.ClientTrips)
                .FirstOrDefault(c => c.Pesel == pesel);
        }

        public void RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
            
        public void CreateNewClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public int GetCurrentMaxId()
        {
            int maxId = _context.Clients.Max(c => c.IdClient);
            if (maxId == null)
                return 0;
            else
                return maxId;
        }
}