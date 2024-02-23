using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.AccAndCardsGen;
using HomeBankingMinHub.Utils.RegistrationVrf;

namespace HomeBankingMinHub.Services.Impl
{
    public class ClientService(IClientRepository clientRepository, IAccountRepository accountRepository) : IClientService
    {
        public List<ClientDTO> getAllClients()
        {
            var clients = clientRepository.GetAllClients();
            var clientsDTO = new List<ClientDTO>();
            foreach (Client client in clients)
            {
                ClientDTO clientDTO = new ClientDTO(client);
                clientsDTO.Add(clientDTO);
            }
            return clientsDTO;
        }
        public ClientDTO getClientById(long id)
        {
            var client = clientRepository.FindById(id);
            var clientDTO = new ClientDTO(client);
            return clientDTO;
        }
        public ClientDTO getClientByEmail(string email)
        {
            var clientDTO = new ClientDTO(clientRepository.FindByEmail(email));
            if (clientDTO.Accounts.Count() == 0)
            {
                Account newAccount = AccountGeneration.NewAccountGeneration(clientRepository.FindByEmail(email));
                accountRepository.Save(newAccount);
            }
            return clientDTO;
        }
        public void createNewClient(NewClientDTO clientDTO)
        {
            Client newClient = NewClientVrf.NewVrfClientDto(clientDTO);
            clientRepository.Save(newClient);
        }
    }
}