using Models.Models.Custom;


namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string Email, string subject, string msg, SmtpSettings settings, string table);
    }
}
