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
using HomeBankingMinHub.Models.Enums;
using HomeBankingMinHub.Utils;

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

        [HttpPost]
        public IActionResult Post([FromBody] NewTransactionsDTO newtransactionDTO)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }

                var fromAccount = _accountsRepository.FindByNumber(newtransactionDTO.FromAccountNumber);
                var toAccount = _accountsRepository.FindByNumber(newtransactionDTO.ToAccountNumber);

                //Validacion de los campos ingresados en el front.
                if (TransactionVerify.NewTransactionFieldValidation(newtransactionDTO, _accountsRepository) != string.Empty) return StatusCode(403, TransactionVerify.NewTransactionFieldValidation(newtransactionDTO, _accountsRepository));

                //Creacion de las transacciones para ambas cuentas.
                _transactionsRepository.Save(TransactionVerify.NewTrasactionFromAccount(newtransactionDTO, _accountsRepository, newtransactionDTO.ToAccountNumber));
                _transactionsRepository.Save(TransactionVerify.NewTransactionToAccount(newtransactionDTO, _accountsRepository, newtransactionDTO.FromAccountNumber));

                //Actualizacion del balance de las cuentas utilizadas
                _accountsRepository.Save(TransactionVerify.BalanceUpdate(fromAccount, newtransactionDTO.Amount * -1));
                _accountsRepository.Save(TransactionVerify.BalanceUpdate(toAccount, newtransactionDTO.Amount));

                return Created("Transferencia realizada", _accountsRepository.FindByNumber(newtransactionDTO.FromAccountNumber));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
