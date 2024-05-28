using Models.Models.Custom;
using System.Net.Mail;
using System.Net;
using Services.Interfaces;
using Microsoft.Extensions.Options;


namespace Services.Services.Email
{
    public class EmailService : IEmailService
    {
       // private readonly SmtpSettings _smtpSettings;

        public EmailService(
            //IOptions<SmtpSettings> smtpSettings
            )
        {
            //_smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string Email, string subject, string msg, SmtpSettings settings)
        {
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
                    Body = msg,
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
