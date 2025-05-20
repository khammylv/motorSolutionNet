using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class Repairs
    {   public int RepairId { get; set; }
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public string EntryDate { get; set; } = string.Empty;
        public string DepartureDate { get; set; } = string.Empty;
        public string RepairDescription { get; set; } = string.Empty;
        public int CompanyCode { get; set; }

    }
}