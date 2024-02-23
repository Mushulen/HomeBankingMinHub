using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Services;
using HomeBankingMinHub.Utils.ClientLoanVrf;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private IClientService _clientService;
        private IAccountService _accountService;
        private ILoanService _loanService;

        public LoansController(IClientService clientService, IAccountService accountService, ILoanService loanService, ITransactionService transactionsService)
        {
            _clientService = clientService;
            _accountService = accountService;
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }
                return Ok(_loanService.getAllLoans());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] LoanApplicationDTO loanApplicationDTO)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }

                LoanApplicationVrf loanApplicationData = new LoanApplicationVrf(loanApplicationDTO, _clientService.getClientByEmail(email), _loanService, _accountService, _clientService);

                //Verificacion de los campos del prestamo.
                if (loanApplicationData.LoanApplicationDataVrf() != string.Empty) { return StatusCode(403, loanApplicationData.ErrorMessage); }

                _loanService.createNewClientLoan(loanApplicationData, loanApplicationDTO);

                return Created("Prestamo Aplicado", _loanService.getById(loanApplicationDTO.LoanId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}