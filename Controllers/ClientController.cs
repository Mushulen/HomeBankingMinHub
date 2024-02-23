using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Utils.RegistrationVrf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using HomeBankingMinHub.Utils.AccAndCardsGen;
using HomeBankingMinHub.Services;

namespace HomeBankingMinHub.Controllers

{
    [Route("api/[controller]")]

    [ApiController]

    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        private IAccountService _accountService;
        private ICardService _cardService;

        public ClientController(IClientService clientService, IAccountService accountsService, ICardService cardService)
        {
            _clientService = clientService;
            _accountService = accountsService;
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clientService.getAllClients());
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
                return Ok(_clientService.getClientById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
   
        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }
                return Ok(_clientService.getClientByEmail(email));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current/accounts")]
        public IActionResult GetCurrentAccounts()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }
                return Ok(_accountService.getAccountsByClientId(_clientService.getClientByEmail(email).Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current/cards")]
        public IActionResult GetCurrentcards()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }
                return Ok(_cardService.getCardsByClientId(_clientService.getClientByEmail(email).Id));
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewClientDTO newClient)
        {
            try
            {
                NewClientVrf newClientData = new NewClientVrf(_clientService);

                //Validacion de los campos ingresados en el front.
                if (!newClientData.NewClientDataVrf(newClient).IsNullOrEmpty()) return StatusCode(400, newClientData.ErrorMessage);

                _clientService.createNewClient(newClient);

                return Created("Creado con exito", newClient);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/accounts")]
        public IActionResult Post()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }

                if (!_accountService.createNewAccount(email).IsNullOrEmpty()) return StatusCode(500, "Ha ocurrido un error");
                
                return Created("Creado con exito", _accountService.getAccountsByClientId(_clientService.getClientByEmail(email).Id).LastOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/cards")]
        public IActionResult Post([FromBody] NewCardDTO newCard)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Usted no ha iniciado sesion");
                }

                CardCreationVrf newVerifiedCard = new CardCreationVrf();

                //Validacion de los campos ingresados en el front.
                if (!newVerifiedCard.CardsVerification(_cardService.getCardsByClientId(_clientService.getClientByEmail(email).Id), newCard).IsNullOrEmpty()) return StatusCode(400, newVerifiedCard.ErrorMessage);

                _cardService.createNewCard(_clientService.getClientByEmail(email),newCard);

                return Created("Creado con exito", _cardService.getCardsByClientId(_clientService.getClientByEmail(email).Id).LastOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}