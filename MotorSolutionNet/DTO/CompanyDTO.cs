using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.DTO
{
    public class CompanyDTO
    {
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string LegalRepresentative { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}