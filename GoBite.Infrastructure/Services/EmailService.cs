using GoBite.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GoBite.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOtpEmailAsync(string toEmail, string otpCode)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            _configuration["Email:DisplayName"]!,
            _configuration["Email:From"]!));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = "Your Password Reset Code";

        message.Body = new TextPart("html")
        {
            Text = $"""
            <h2>Password Reset Request</h2>
            <p>Your verification code is:</p>
            <h1 style="letter-spacing: 8px; font-size: 32px;">{otpCode}</h1>
            <p>This code expires in 5 minutes.</p>
            <p>If you did not request this, please ignore this email.</p>
            """
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            _configuration["Email:Host"]!,
            int.Parse(_configuration["Email:Port"]!),
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["Email:Username"]!,
            _configuration["Email:Password"]!);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
