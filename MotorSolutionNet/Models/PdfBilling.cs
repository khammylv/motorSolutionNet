using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class PdfBilling
    {
        public int BillingId { get; set; }
        public int RepairId { get; set; }
        public string BillingDate { get; set; }
        public decimal Amount { get; set; }
        public string EntryDate { get; set; }
        public string DepartureDate { get; set; }
        public string RepairStatus { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public string ClientName { get; set; }  
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }  
        public string CompanyEmail { get; set; }  
        public string CompanyPhone { get; set; }    
        public string Nit { get; set; }            
    }
}