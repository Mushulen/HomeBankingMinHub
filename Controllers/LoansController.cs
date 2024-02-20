using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils;
using HomeBankingMinHub.Utils.ClientLoanVrf;
using HomeBankingMinHub.Utils.DtoLoad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IAccountRepository _accountsRepository;
        private ILoanRepository _loanRepository;
        private IClientLoanRepository _clientLoanRepository;
        private ITransactionsRepository _transactionsRepository;

        public LoansController(IClientRepository clientRepository, IAccountRepository accountsRepository, ILoanRepository loanRepository, IClientLoanRepository clientLoanRepository, ITransactionsRepository transactionsRepository)
        {
            _clientRepository = clientRepository;
            _accountsRepository = accountsRepository;
            _loanRepository = loanRepository;
            _clientLoanRepository = clientLoanRepository;
            _transactionsRepository = transactionsRepository;
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
                var loans = _loanRepository.GetAllLoans();
                return Ok(LoanDtoLoader.AllLoans(loans));
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

                Client client = _clientRepository.FindByEmail(email);
                LoanApplicationVrf loanApplicationData = new LoanApplicationVrf(loanApplicationDTO, client, _loanRepository, _accountsRepository, _clientRepository);

                if (loanApplicationData.LoanApplicationDataVrf() != string.Empty) { return StatusCode(403, loanApplicationData.ErrorMessage); }

                var toAccount = _accountsRepository.FindByNumber(loanApplicationDTO.ToAccountNumber);

                _clientLoanRepository.Save(loanApplicationData.ClientLoanVerifiedGeneration());
                _transactionsRepository.Save(loanApplicationData.LoanTransactionGeneration());
                _accountsRepository.Save(TransactionVerify.BalanceUpdate(toAccount, loanApplicationDTO.Amount));

                return Created("Prestamo Aplicado", _loanRepository.FindById(loanApplicationDTO.LoanId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}