﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Mail;

using MailKit.Net.Smtp;
using MimeKit;

using System.Text;
using System.Threading.Tasks;

using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.Utilities
    {
    public class EmailService : IEmailService
        {
        private readonly string _smtpServer;    //= "smtp-mail.outlook.com"
        private readonly int _smtpPort;         // = 587
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
            using (var client = new SmtpClient())
                {
                client.Connect(_smtpServer, _smtpPort, true);
                client.Authenticate(_smtpUserName, _smtpPassword);
                client.Send(emailMessage);
                client.Disconnect(true);

                };
            }
        }

    }