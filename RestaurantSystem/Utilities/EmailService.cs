using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;


using MailKit.Net.Smtp;
using MimeKit;

using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.Utilities
    {
    public class EmailService : IEmailService
        {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUserName;
        private readonly string _smtpPassword;

        public EmailService(string smtpServer, int smtpPort, string smtpUserName, string smtpPassword)
            {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUserName = smtpUserName;
            _smtpPassword = smtpPassword;

            }

        public void SendEmail(string toAddress, string subject, string body, string attachmentPath = null)
            {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Restaurant", _smtpUserName));
            emailMessage.To.Add(new MailboxAddress("Recipient", toAddress));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            if (!string.IsNullOrEmpty(attachmentPath))
                {
                builder.Attachments.Add(attachmentPath);
                }

            emailMessage.Body = builder.ToMessageBody();
            try
                {
                using (var client = new SmtpClient())
                    {
                    client.Connect(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(_smtpUserName, _smtpPassword);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                    }

                ;
                Console.WriteLine("Email sent successfully.");
                }
            catch (SmtpProtocolException ex)
                {
                Console.WriteLine($"Protocol error while sending email: {ex.Message}");
                }
            catch (SocketException ex)
                {
                Console.WriteLine($"Network error {ex.Message}");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

    }
