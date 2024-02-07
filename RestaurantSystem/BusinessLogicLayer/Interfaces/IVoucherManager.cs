using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer.Interfaces
    {
    public interface IVoucherManager
        {
        IVoucher CreateCustomerVoucher(Order order);
        IVoucher CreateRestaurantVoucher(Order order);
        void PrintVoucher(IVoucher voucher);
        void SendVoucherByEmail(IVoucher voucher, string emailAddress);
        }
    }
