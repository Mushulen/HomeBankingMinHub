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

    public class ClientController : ControllerBase
    {
        private IClientRepository _clientRepository;

        private IAccountRepository _accountsRepository;

        private ICardRepository _cardRepository;

        public ClientController(IClientRepository clientRepository, IAccountRepository accountsRepository, ICardRepository cardRepository)
        {
            _clientRepository = clientRepository;
            _accountsRepository = accountsRepository;
            _cardRepository = cardRepository;
        }

        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid();
                }

                Client client = _clientRepository.FindByEmail(email);

                if (client.Accounts.Count() == 0)
                {
                    Account newAccount = AccountGeneration.NewAccountGeneration(client);
                    _accountsRepository.Save(newAccount);
                }
                ClientDTO clientDTO = ClientDtoLoader.CurrentClient(client);
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current/accounts")]
        public IActionResult GetCurrentAccounts()
        {

            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }

            Client client = _clientRepository.FindByEmail(email);

            try
            {
                var accounts = _accountsRepository.GetAccountsByClient(client.Id);
                return Ok(AccountDtoLoader.CurrentClientAccounts(accounts));
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current/cards")]
        public IActionResult GetCurrentcards()
        {

            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }

            Client client = _clientRepository.FindByEmail(email);

            try
            {
                var cards = _cardRepository.GetCardsByClient(client.Id);
                return Ok(CardDtoLoader.CurrentClientCards(cards));
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
                NewClientDTO newclient = newClient;
                NewClientVrf newClientData = new NewClientVrf(_clientRepository);

                if (!newClientData.NewClientDataVrf(newClient).IsNullOrEmpty()) return StatusCode(403, newClientData.ErrorMessage);

                _clientRepository.Save(newClientData.NewVrfClientDto(newclient));

                return Created("Creado con exito", newclient);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/accounts")]
        public IActionResult Post()
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }
            try
            {
                Client client = _clientRepository.FindByEmail(email);
                var accounts = _accountsRepository.GetAccountsByClient(client.Id);

                if (accounts.Count() >= 3)
                {
                    return StatusCode(400, "No puede tener mas de 3 cuentas.");
                }

                Account newAccount = AccountGeneration.NewAccountGeneration(client);
                _accountsRepository.Save(newAccount);
                return Created("Creado con exito", newAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/cards")]
        public IActionResult Post([FromBody] NewCardDTO newCard)
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }
            try
            {
                Client client = _clientRepository.FindByEmail(email);
                var cards = _cardRepository.GetCardsByClient(client.Id);

                //Verificacion de que el cliente no tenga mas de 3 tarjetas de un tipo, o si ya posee una tarjeta del mismo color de la que esta intentando crear.
                if (!CardCreationVrf.CardsVerification(cards, newCard).IsNullOrEmpty()) return StatusCode(400, CardCreationVrf.CardsVerification(cards, newCard));

                Card newGeneratedCard = CardGeneration.NewCardGeneration(client, newCard);
                _cardRepository.Save(newGeneratedCard);
                return Created("Creado con exito", newGeneratedCard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}