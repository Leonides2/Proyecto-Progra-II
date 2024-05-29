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

        public async Task SendEmailAsync(string Email, string subject, string msg, SmtpSettings settings, string table)
        {
            string body = "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                "<head>\r\n\r\n   " +
                " <style>\r\n        " +
                "table {\r\n         " +
                " font-family: arial, sans-serif;\r\n         " +
                " border-collapse: collapse;\r\n          " +
                "width: 100%;\r\n        }\r\n        \r\n        " +
                "td, th {\r\n         " +
                " border: 1px solid #dddddd;\r\n         " +
                " text-align: left;\r\n          " +
                "padding: 8px;\r\n        }\r\n        \r\n        " +
                "tr:nth-child(even) {\r\n         " +
                " background-color: #dddddd;\r\n        }\r\n\r\n        " +
                "body{\r\n            width: 100%\r\n        }\r\n\r\n       " +
                " h2{\r\n            " +
                "font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\r\n        }\r\n       " +
                " h3{\r\n            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\r\n        }\r\n       " +
                " p{\r\n            word-wrap: break-word; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;\r\n        }\r\n        " +
                "</style>\r\n" +
                "</head>\r\n" +
                "<body\">\r\n    " +
                "<h2>"+ subject +" </h2>\r\n   " +
                "<h3>"+ msg +"</h3>\r\n    " +
                table
                +
                "<p> Por favor no responda a este correo, es un correo generado automaticamente</p>\r\n" +
                "</body>\r\n" +
                "</html>";
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
