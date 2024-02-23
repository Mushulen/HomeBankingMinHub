using HomeBankingMinHub.Models.DTO;

namespace HomeBankingMinHub.Services
{
    public interface IClientService
    {
        List<ClientDTO> getAllClients();
        ClientDTO getClientById(long id);
        ClientDTO getClientByEmail(string email);
        void createNewClient (NewClientDTO client);
    }
}