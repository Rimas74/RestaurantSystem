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

        public IVoucher CreateRestaurantVoucher(Order order, int voucherId)
            {
            var restaurantVoucher = new RestaurantVoucher(order, voucherId);

            _voucherDataAccess.SaveRestaurantVoucher(restaurantVoucher);
            Console.WriteLine("Printing Restaurant Voucher to console:\n");
            _printService.PrintToConsole(restaurantVoucher.PrintVoucher());

            return restaurantVoucher;
            }

        public void SendVoucherByEmail(IVoucher voucher, string emailAddress)
            {
            string voucherContent = voucher.PrintVoucher();
            _emailService.SendEmail(emailAddress, "Your Voucher", voucherContent);
            }
        }
    }
