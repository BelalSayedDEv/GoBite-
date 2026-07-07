namespace GoBite.Application.Interfaces.Service;

public interface IEmailService
{
    Task SendOtpEmailAsync(string toEmail, string otpCode);
}
