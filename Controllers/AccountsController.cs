using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.DtoLoad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                return Ok(AccountDtoLoader.CurrentClientAccounts(accounts));
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
                return Ok(AccountDtoLoader.CurrentClientAccountById(account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
