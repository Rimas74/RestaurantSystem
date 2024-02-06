using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.Mocks
    {
    public class EmailServiceMock : IEmailService
        {

        public void SendEmail(string toAddress, string subject, string body, string attachmentPath = null)
            {
            Console.WriteLine($"Mock Email Sent to {toAddress}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            if (!string.IsNullOrEmpty(attachmentPath))
                {
                Console.WriteLine($"Attachment: {attachmentPath}");
                }
            }
        }
    }
