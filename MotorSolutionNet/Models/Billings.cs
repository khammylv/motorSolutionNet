using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class Billings
    {
        public int BillingId { get; set; }
        public int RepairId { get; set; }
        public int ClientId { get; set; }
        public string BillingDate { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int CompanyCode { get; set; }
    
    }
}