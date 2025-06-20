using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class CompanySummaryResult
    {
        public Dictionary<string, int> Resumen { get; set; }

        public CompanySummaryResult()
        {
            Resumen = new Dictionary<string, int>
        {
            { "total_users", 0 },
            { "total_clients", 0 },
            { "total_vehicles", 0 },
            { "total_bills", 0 }
        };
        }
    }
}