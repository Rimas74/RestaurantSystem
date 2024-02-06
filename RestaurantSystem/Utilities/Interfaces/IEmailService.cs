using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Utilities.Interfaces
    {
    public interface IEmailService
        {
        void SendEmail(string toAddress, string subject, string body, string attachmentPath = null);
        }
    }
