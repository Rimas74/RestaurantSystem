using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer.Interfaces
    {
    public interface IVoucherManager
        {
        IVoucher CreateCustomerVoucher(Order order, int voucherId);
        IVoucher CreateRestaurantVoucher(Order order, int voucherId);
        void PrintVoucher(IVoucher voucher);
        void SendVoucherByEmail(IVoucher voucher, string emailAddress);
        }
    }
