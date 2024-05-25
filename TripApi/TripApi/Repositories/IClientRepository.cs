namespace TripApi.Repositories;

using TripApi.Models;
public interface IClientRepository
{
    Client GetClientById(int idClient);
    void RemoveClient(Client client);
    Client GetClientByPeselNumber(string pesel);
    void CreateNewClient(Client client);
    int GetCurrentMaxId();
}