using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Services
{
    public class ClientServices
    {
        private readonly ClientData _clientData;
        private readonly PaginationHelper _clientPagination;
        public ClientServices()
        {
            _clientData = new ClientData();
            _clientPagination = new PaginationHelper();
        }

        public bool AddClient(Client client)
        {
            bool isValid = ConfigManager.IsObjectValid(client);
            if (!isValid)
            {
                throw new Exception("El cliente tiene campos Vacios");

            }
            return _clientData.AddClient(client);
        }
        public bool UpdateClient(ClientDTO client)
        {
            bool isValid = ConfigManager.IsObjectValid(client);
            if (!isValid)
            {
                throw new Exception("El cliente tiene campos Vacios");

            }
            return _clientData.UpdateClient(client);
        }
        public Client GetClientValidation(int? clientId = null, string email = null, string identification = null, int? companyCode = null)
        {
            return _clientData.GetClientValidation(clientId, email, identification, companyCode);
        }
        public Object GetClientByCompany(int companyCode, int pageIndex, int pageSize)
        {
            var clients = _clientData.GetClientByCompany(companyCode);
            return _clientPagination.Paginate(clients, pageIndex, pageSize);
        }
        public Client GetClient(int clientId)
        {
            var client = _clientData.GetClient(clientId);
           
            return client;
        }
        public bool DeleteClient(int clientId)
        {
            return _clientData.DeleteClient(clientId);
        }
        }
}