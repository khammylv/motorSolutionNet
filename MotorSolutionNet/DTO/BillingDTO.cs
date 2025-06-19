using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.DTO
{
    public class BillingDTO
    {
        public int BillingId { get; set; }
        public int RepairId { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        
        public int CompanyCode { get; set; }
    }
}