using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;

namespace MotorSolutionNet.Data
{
    public class EmailSendData
    {
        private readonly CompanyData _companyData;
        public EmailSendData()
        {
            _companyData = new CompanyData();
        }

        public EmailSend GetEmailSend(int? companyCode = null, EmailSend emailData = null)
        {
            var company = _companyData.GetCompany(companyCode);
            if (company != null)
            {
                return new EmailSend
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    PassportEmail = company.PasswordEmail,
                    EmailSubject = emailData.EmailSubject,
                    EmailBody = emailData.EmailBody,
                    EmailReceiver = emailData.EmailReceiver
                };
            }
            return null;
        }
    }
}