using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Services
{
    public class BillingService
    {
        private readonly BillingData _billingData;
        private readonly PaginationHelper _billingPagination;
        public BillingService() {
            _billingData = new BillingData();
            _billingPagination = new PaginationHelper();
        }

        public bool AddBilling(BillingDTO billing)
        {
            bool isValid = ConfigManager.IsObjectValid(billing);
            if (!isValid)
            {
                throw new Exception("El vehiculo tiene campos Vacios");

            }
            return _billingData.AddBilling(billing);
        }
        public Billings GetBilling(int billingId)
        {
            return _billingData.GetBilling(billingId);
        }
        public Billings GetBillingByRepairId(int repairId)
        {
            return _billingData.GetBillingByRepairId(repairId);
        }
        public Object GetFullBilling(int billingId)
        {
            return _billingData.GetFullBilling(billingId);
        }
        public Object GetBillingsByCompany(int companyCode, int pageIndex, int pageSize)
        {
            var billing = _billingData.GetBillingsByCompany(companyCode);
            return _billingPagination.Paginate(billing, pageIndex, pageSize);

        }
        public Object GetBillingsByClient(int clientId, int pageIndex, int pageSize)
        {
            var billing = _billingData.GetBillingsByClient(clientId);
            return _billingPagination.Paginate(billing, pageIndex, pageSize);
        }

    }
}