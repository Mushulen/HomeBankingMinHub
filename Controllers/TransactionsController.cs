using HomeBankingMinHub.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using HomeBankingMinHub.Utils;
using HomeBankingMinHub.Services;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private IAccountService _accountService;
        private ITransactionService _transactionService;

        public TransactionsController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
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

                TransactionVerify TransactionDataVerify = new TransactionVerify();

                //Validacion de los campos ingresados en el front.
                if (TransactionDataVerify.NewTransactionFieldValidation(newtransactionDTO, _accountService) != string.Empty) return StatusCode(403, TransactionDataVerify.ErrorMessage);

                _transactionService.createNewTransaction(newtransactionDTO);

                return Created("Transferencia realizada", _accountService.getByNumber(newtransactionDTO.FromAccountNumber));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}