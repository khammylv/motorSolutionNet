using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int ClientId { get; set; } // Foreign key to Client
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string EntryDate { get; set; } = string.Empty;
        public string DepartureDate { get; set; } = string.Empty;
        public string RepairDescription { get; set; } = string.Empty;
        public int CompanyCode { get; set; }
        public string Plate { get; set; } = string.Empty;
    }
}