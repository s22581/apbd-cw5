using TripApi.Repositories;
using TripApi.Models;
using TripApi.Models.DTOs;

namespace TripApi.Services;
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IClientRepository _clientRepository;
        public TripService(ITripRepository tripRepository, IClientRepository clientRepository)
        {
            _tripRepository = tripRepository;
            _clientRepository = clientRepository;
        }

        public List<GetTripDto> GetSortedTrips()
        {
            var trips = _tripRepository.GetTrips();
            var dtosList = trips.Select(t => new GetTripDto()
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new GetCountryDto()
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(c => new GetClientDto()
                {
                    FirstName = c.IdClientNavigation.FirstName,
                    LastName = c.IdClientNavigation.LastName,
                }).ToList(),
            }).OrderByDescending(t => t.DateFrom).ToList();
            return dtosList;
        }

        public void AssignClientToTrip(int idTrip, AssignClientToTripDto dto)
        {
            var client = _clientRepository.GetClientByPeselNumber(dto.Pesel);
            if (client == null)
            {
                client = new Client()
                {
                    IdClient= _clientRepository.GetCurrentMaxId() + 1,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Telephone = dto.Telephone,
                    Pesel = dto.Pesel,
                    ClientTrips = new List<ClientTrip>()
                };
                _clientRepository.CreateNewClient(client);
                
            }
            if(client.ClientTrips.Any(t => t.IdTrip == idTrip)){
                throw new Exception($"Client already assigned to the trip with Id: {idTrip}");
            }
            var trip = _tripRepository.GetTripById(idTrip);
            if(trip == null)
            {
                throw new Exception($"Trip with Id: {idTrip} does not exist");
            }
            var clientTrip = new ClientTrip()
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = dto.PaymentDate
            };
            _tripRepository.AddTripToClientTrips(clientTrip);
        }

    }