using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.DTO
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
       
    }
}