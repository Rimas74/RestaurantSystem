using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.DataAccessLayer;
using RestaurantSystem.Utilities;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.BusinessLogicLayer
    {
    public class VoucherManager : IVoucherManager
        {
        private readonly VoucherDataAccess _voucherDataAccess;
        private readonly IPrintService _printService;
        private readonly IEmailService _emailService;

        public VoucherManager(VoucherDataAccess voucherDataAccess, IPrintService printService, IEmailService emailService)
            {
            _voucherDataAccess = voucherDataAccess;
            _printService = printService;
            _emailService = emailService;
            }


        public IVoucher CreateCustomerVoucher(Order order, int voucherId)
            {
            var customerVoucher = new CustomerVoucher(order, voucherId);
            return customerVoucher;
            }

        public IVoucher CreateRestaurantVoucher(Order order)
            {
            int voucherId = VoucherIdTracker.GetNextVoucherId();
            var restaurantVoucher = new RestaurantVoucher(order, voucherId);

            _voucherDataAccess.SaveRestaurantVoucher(restaurantVoucher);
            Console.WriteLine("Printing Restaurant Voucher to console:\n");
            PrintVoucher(restaurantVoucher);

            CreateCustomerVoucher(order, voucherId);

            return restaurantVoucher;
            }

        public void SendVoucherByEmail(IVoucher voucher, string emailAddress)
            {
            string voucherContent = voucher.PrintVoucher();
            _emailService.SendEmail(emailAddress, "Your Voucher", FormatForHtml(voucherContent));
            }

        private string FormatForHtml(string content)
            {
            return content.Replace(Environment.NewLine, "<br>");
            }

        public void PrintVoucher(IVoucher voucher)
            {
            var voucherContent = voucher.PrintVoucher();
            _printService.PrintToConsole(voucherContent);
            }

        public void GetSenderEmail()
            {
            string senderEmail = _emailService.SenderEmail;
            }
        }
    }
