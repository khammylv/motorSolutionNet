using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.DTO
{
    public class RepairsDTO
    {
        public int RepairId { get; set; }
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CompanyCode { get; set; }
    }
}