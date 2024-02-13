﻿using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
        Client FindByEmail(string email);
    }
}