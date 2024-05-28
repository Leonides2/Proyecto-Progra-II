using Models.Models.Custom;
using System.Net.Mail;
using System.Net;
using Services.Interfaces;
using Microsoft.Extensions.Options;


namespace Services.Services.Email
{
    public class EmailService : IEmailService
    {

        public EmailService()
        {
            
        }

        public async Task SendEmailAsync(string Email, string subject, string msg, SmtpSettings settings)
        {
            string body = "<!DOCTYPE html>\r\n<html>\r\n" +
                "<head>\r\n  " +
                "<link rel='stylesheet' type='text/css' media='screen' href='main.css'>\r\n" +
                "</head>\r\n" +
                "<body style=\"width: 100%;\">\r\n    " +
                "<h2 style=\"font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\">"+ subject +" </h2>\r\n    " +
                "<h3 style=\"font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\"> "+ msg +"</h3>\r\n\r\n    " +
                "<p style=\"word-wrap: break-word; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\"> Por favor no responda a este correo, es un correo generado automaticamente</p>\r\n" +
                "</body>\r\n</html>";
            try
            {
                var client = new SmtpClient(settings.Server, settings.Port)
                {
                    Credentials = new NetworkCredential(settings.Username, settings.Password),
                    EnableSsl = true,
                };


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(settings.Username!),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add( new MailAddress(Email));

                await client.SendMailAsync(mailMessage);

            }
            catch (Exception ex) 
            {
                throw new Exception("Can't send the email", ex);
            }
            

           
        }
    }
}
