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

                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    return Forbid("No existe el cliente");
                }

                if (newtransactionDTO.FromAccountNumber == string.Empty || newtransactionDTO.ToAccountNumber == string.Empty)
                {
                    return Forbid("Cuenta de origen o cuenta de destino no proporcionada.");
                }

                if (newtransactionDTO.FromAccountNumber == newtransactionDTO.ToAccountNumber)
                {
                    return Forbid("No se permite la transferencia a la misma cuenta.");
                }

                if (newtransactionDTO.Amount == 0 || newtransactionDTO.Description == string.Empty)
                {
                    return Forbid("Monto o descripcion no proporcionados.");
                }

                //buscamos las cuentas
                Account fromAccount = _accountsRepository.FinByNumber(newtransactionDTO.FromAccountNumber);
                if (fromAccount == null)
                {
                    return Forbid("Cuenta de origen no existe");
                }

                //controlamos el monto
                if (fromAccount.Balance < newtransactionDTO.Amount)
                {
                    return Forbid("Fondos insuficientes");
                }

                //buscamos la cuenta de destino
                Account toAccount = _accountsRepository.FinByNumber(newtransactionDTO.ToAccountNumber);
                if (toAccount == null)
                {
                    return Forbid("Cuenta de destino no existe");
                }

                //demas logica para guardado
                //comenzamos con la inserrción de las 2 transacciones realizadas
                //desde toAccount se debe generar un debito por lo tanto lo multiplicamos por -1
                _transactionsRepository.Save(new Transactions
                {
                    Type = TransactionType.DEBIT,
                    Amount = newtransactionDTO.Amount * -1,
                    Description = newtransactionDTO.Description + " " + toAccount.Number,
                    AccountId = fromAccount.Id,
                    Date = DateTime.Now,
                });

                //ahora una credito para la cuenta fromAccount
                _transactionsRepository.Save(new Transactions
                {
                    Type = TransactionType.CREDIT,
                    Amount = newtransactionDTO.Amount,
                    Description = newtransactionDTO.Description + " " + fromAccount.Number,
                    AccountId = toAccount.Id,
                    Date = DateTime.Now,
                });

                //seteamos los valores de las cuentas, a la ccuenta de origen le restamos el monto
                fromAccount.Balance = fromAccount.Balance - newtransactionDTO.Amount;
                //actualizamos la cuenta de origen
                _accountsRepository.Save(fromAccount);

                //a la cuenta de destino le sumamos el monto
                toAccount.Balance = toAccount.Balance + newtransactionDTO.Amount;
                //actualizamos la cuenta de destino
                _accountsRepository.Save(toAccount);


                return Created("Transferencia realizada", fromAccount);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
