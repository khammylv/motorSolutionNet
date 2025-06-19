using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class RepairDetails
    {
        public int RepairDetailsId { get; set; }
        public int RepairId { get; set; }
        public decimal Price { get; set; }
        public string RepairServices { get; set; } = string.Empty;
        public string RepairDescription { get; set; } = string.Empty;
    }
}

