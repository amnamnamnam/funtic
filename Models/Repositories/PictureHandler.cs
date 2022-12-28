using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace FUNTIK.Models.Repositories
{
    public interface IPictureHandler
    {
        void Share(string picture, string details);
    }

    public class PictureSender : IPictureHandler
    {
        public void Share(string picture, string details)
        {
            const string smtpHostAddress = "smtp.gmail.com";
            const string adminEmailAddress = "funtiknoreply@gmail.com";
            const string adminEmailPassword = "Amnamnam2912#!";

            var smtpClient = new SmtpClient(smtpHostAddress)
            {
                Port = 587,
                Credentials = new NetworkCredential(adminEmailAddress, adminEmailPassword),
            };

            smtpClient.Send(adminEmailAddress, "fornatasha12345@gmail.com", "subject", "body");
        }


    }
}
