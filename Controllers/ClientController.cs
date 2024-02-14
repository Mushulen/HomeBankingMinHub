using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Utils.ClientDtoLoad;
using HomeBankingMinHub.Repositories.Interface;
using HomeBankingMinHub.Utils.RegistrationVrf;
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

    public class ClientController : ControllerBase
    {
        private IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
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
                if (client == null)
                {
                    return Forbid();
                }
                ClientDTO clientDTO = ClientDtoLoader.CurrentClient(client);
                return Ok(clientDTO);
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

                if (!newClientData.NewClientDataVrf(newClient).IsNullOrEmpty()) return StatusCode(400, newClientData.ErrorMessage);

                _clientRepository.Save(newClientData.NewVrfClientDto(newclient));

                return Created("Creado con exito",newclient);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}