using HomeBankingMinHub.Models;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client FindById(long id);
        Client FindByEmail(string email);
        void Save(Client client);
    }
}