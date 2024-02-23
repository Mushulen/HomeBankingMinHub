using HomeBankingMinHub.Models;
using HomeBankingMinHub.Models.DTO;
using HomeBankingMinHub.Services;
using System.Text.RegularExpressions;

namespace HomeBankingMinHub.Utils.RegistrationVrf
{
    public class NewClientVrf (IClientService clientService)
    {
        public string ErrorMessage = string.Empty;
        private string specialChars = @"[^a-zA-Z]"; //Regular expresion que excluye de la a-z minuscula y mayuscula.

        //Verificacion de los campos del cliente nuevo.
        public string NewClientDataVrf(NewClientDTO NewClient)
        {
            if (string.IsNullOrEmpty(NewClient.FirstName) || Regex.IsMatch(NewClient.FirstName, specialChars)) { ErrorMessage = "Nombre Inválido"; }
            else if (string.IsNullOrEmpty(NewClient.LastName) || Regex.IsMatch(NewClient.LastName, specialChars)) { ErrorMessage = "Apellido Inválido"; }
            else if (string.IsNullOrEmpty(NewClient.Email)) { ErrorMessage = "Email Inválido"; }
            else if (!AlreadyExistingEmail(NewClient.Email)) { ErrorMessage = "Email ya existente"; }
            else if (string.IsNullOrEmpty(NewClient.Password)) { ErrorMessage = "Contraseña Inválida"; }
            return ErrorMessage;
        }

        //Creacion del nuevo cliente.
        public static Client NewVrfClientDto(NewClientDTO newclient)
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
            var clients = clientService.getAllClients(); 
            foreach (var client in clients)
            {
                if (client.Email == Email) return false;
            }
            return true;
        }
    }
}