using Microsoft.Extensions.Configuration;
using SocialSecurity.Shared.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SocialSecurity.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var host = smtpSettings["Host"] ?? throw new InvalidOperationException("SMTP Host is not configured");
            var port = smtpSettings["Port"] ?? throw new InvalidOperationException("SMTP Port is not configured");
            var userName = smtpSettings["UserName"] ?? throw new InvalidOperationException("SMTP Username is not configured");
            var password = smtpSettings["Password"] ?? throw new InvalidOperationException("SMTP Password is not configured");
            var enableSsl = smtpSettings["EnableSSL"] ?? "false";

            using (var client = new SmtpClient(host, int.Parse(port)))
            {
                client.Credentials = new NetworkCredential(userName, password);
                client.EnableSsl = bool.Parse(enableSsl);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(userName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendVerificationCodeAsync(string email, string code)
        {
            var subject = "Email Verification Code";
            var body = $@"
                <div style='font-family: Arial, sans-serif; text-align: center;'>
                    <h2>Email Verification</h2>
                    <p>Your verification code is:</p>
                    <h3 style='background: #007bff; color: white; padding: 10px 20px; display: inline-block;'>{code}</h3>
                    <p>This code will expire in 10 minutes.</p>
                    <p>If you didn't request this, please ignore this email.</p>
                </div>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var subject = "Password Reset Request";
            var body = $@"
                <div style='font-family: Arial, sans-serif; text-align: center; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>Password Reset</h2>
                    <p style='color: #666; margin-bottom: 20px;'>Click the button below to reset your password:</p>
                    <div style='margin: 30px 0;'>
                        <a href='{resetLink}' 
                           style='background-color: #007bff; 
                                  color: white; 
                                  padding: 12px 24px; 
                                  text-decoration: none; 
                                  border-radius: 4px; 
                                  display: inline-block; 
                                  font-weight: bold;'>
                            Reset Password
                        </a>
                    </div>
                    <p style='color: #666; font-size: 14px;'>If the button above doesn't work, copy and paste this link into your browser:</p>
                    <p style='color: #007bff; word-break: break-all; font-size: 14px;'>{resetLink}</p>
                    <p style='color: #666; font-size: 14px; margin-top: 20px;'>This link will expire in 1 hour.</p>
                    <p style='color: #666; font-size: 14px;'>If you didn't request this, please ignore this email.</p>
                </div>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordResetConfirmationAsync(string email)
        {
            var subject = "Password Reset Confirmation";
            var body = $@"
                <div style='font-family: Arial, sans-serif; text-align: center;'>
                    <h2>Password Reset Successful</h2>
                    <p>Your password has been successfully reset.</p>
                    <p>If you didn't make this change, please contact support immediately.</p>
                </div>";

            await SendEmailAsync(email, subject, body);
        }
    }
}