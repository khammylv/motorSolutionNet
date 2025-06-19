using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class StatusSummaryResult
    {
        public Dictionary<string, int> Status { get; set; } = new Dictionary<string, int>();
    }
}