using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Repositories.Interface;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace HomeBankingMinHub.Utils.RegistrationVrf
{
    public class NewClientVrf (IClientRepository clientRepository)
    {
        public string ErrorMessage = string.Empty;
        private string specialChars = @"[^a-zA-Z]"; //Coleccion de caracteres Regex que solo acepta letras de la a-z minuscula y A-Z mayusculas.
        private IClientRepository _clientrepository = clientRepository;

        //Verificacion de los campos del cliente nuevo.
        public string NewClientDataVrf(NewClientDTO NewClient)
        {
            if (string.IsNullOrEmpty(NewClient.FirstName) || Regex.IsMatch(NewClient.FirstName, specialChars)) { ErrorMessage += " Nombre Inválido"; }
            if (string.IsNullOrEmpty(NewClient.LastName) || Regex.IsMatch(NewClient.LastName, specialChars)) { ErrorMessage += " Apellido Inválido"; }
            if (string.IsNullOrEmpty(NewClient.Email)) { ErrorMessage += " Email Inválido"; }
            if (!AlreadyExistingEmail(NewClient.Email)) { ErrorMessage += " Email ya existente"; }
            if (string.IsNullOrEmpty(NewClient.Password)) { ErrorMessage += " Contraseña Inválida"; }
            return ErrorMessage;
        }

        //Creacion del nuevo cliente.
        public Client NewVrfClientDto(NewClientDTO newclient)
        {
            var newClient = new Client
            {
                FirstName = newclient.FirstName,
                LastName = newclient.LastName,
                Email = newclient.Email,
                Password = PasswordManagement.EncryptPassword(newclient.Password)
            };
            return (newClient);
        }

        //Verificacion si el email ya esta registrado.
        private bool AlreadyExistingEmail(string Email)
        {
            var clients = _clientrepository.GetAllClients(); 
            foreach (var client in clients)
            {
                if (client.Email == Email) return false;
            }
            return true;
        }
    }
}
