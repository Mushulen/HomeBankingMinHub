using HomeBankingMinHub.dtos;
using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountRepository _accountsRepository;
        public AccountsController(IAccountRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }
        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var accounts = _accountsRepository.GetAllAccounts();
                var accountsDTO = new List<AccountDTO>();

                foreach (Account account in accounts)
                {
                    var newaccountsDTO = new AccountDTO
                    {
                        Id = account.Id,
                        Number = account.Number,
                        CreationDate = account.CreationDate,
                        Balance = account.Balance,
 
                    };
                    accountsDTO.Add(newaccountsDTO);
                }
                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var account = _accountsRepository.FindById(id);
                if (account == null)
                {
                    return Forbid();
                }
                var accountDTO = new AccountDTO
                {
                    Id = account.Id,
                    Number = account.Number,
                    CreationDate = account.CreationDate,
                    Balance = account.Balance,
                };
                return Ok(accountDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
