using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class EmailSend
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string PassportEmail { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
        public string EmailReceiver { get; set; } = string.Empty;
    }
}