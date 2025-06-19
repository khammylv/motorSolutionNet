using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Data
{
    public class EmailSendData
    {
        private readonly CompanyData _companyData;
        public EmailSendData()
        {
            _companyData = new CompanyData();
        }

        private EmailSend GetEmailSend( EmailSend emailData = null)
        {
            var company = _companyData.GetCompanyVal(emailData.CompanyCode);
            if (company != null)
            {
                string decrypted = EncryptionHelper.Decrypt(company.PasswordEmail);
                return new EmailSend
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    PassportEmail = decrypted,
                    EmailSubject = emailData.EmailSubject,
                    EmailBody = emailData.EmailBody,
                    EmailReceiver = emailData.EmailReceiver
                };
            }
            return null;
        }
        public void TestObject(EmailSend emailSend)
        {
            
            var companyMailObject = GetEmailSend(emailSend);
            System.Diagnostics.Debug.WriteLine("Email: " + companyMailObject.CompanyEmail);

        }

        public void SendEmail( EmailSend emailSend)
        {
            var companyMailObject = GetEmailSend(emailSend);
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(companyMailObject.CompanyEmail, companyMailObject.CompanyName)
            };
            mail.To.Add(companyMailObject.EmailReceiver);
            mail.Subject = companyMailObject.EmailSubject;
            mail.Body = companyMailObject.EmailBody;
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Port = 587,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential(companyMailObject.CompanyEmail, companyMailObject.PassportEmail)
            };
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
    }
}