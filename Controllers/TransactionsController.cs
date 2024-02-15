using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Utils.DtoLoad;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.RegistrationVrf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using HomeBankingMinHub.Repositories;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using HomeBankingMinHub.Utils.AccAndCardsGen;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private IClientRepository _clientRepository;

        private IAccountRepository _accountsRepository;

        private ITransactionsRepository _transactionsRepository;

        public TransactionsController(IClientRepository clientRepository, IAccountRepository accountsRepository, ITransactionsRepository transactionsRepository)
        {
            _clientRepository = clientRepository;
            _accountsRepository = accountsRepository;
            _transactionsRepository = transactionsRepository;
        }

        /* [HttpPost]
         public IActionResult Post([FromBody] NewClientDTO newClient)
         {
         }*/
    }
}
