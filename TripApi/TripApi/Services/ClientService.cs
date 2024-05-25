namespace TripApi.Services;

using TripApi.Repositories;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public void RemoveClient(int idClient)
    {
        var client = _clientRepository.GetClientById(idClient);
        if(client == null)
        {
            throw new Exception($"Client with Id: {idClient}, does not exist");
        }
        if (client.ClientTrips.Any())
        {
            throw new Exception("Cannot remove client with trips");
        }
        _clientRepository.RemoveClient(client);
    }
}