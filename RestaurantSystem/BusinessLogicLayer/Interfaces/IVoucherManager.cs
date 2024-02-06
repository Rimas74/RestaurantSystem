using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer.Interfaces
    {
    public interface IVoucherManager
        {
        IVoucher CreateCustomerVoucher(Order order, int voucherId);
        IVoucher CreateRestaurantVoucher(Order order, int voucherId);
        }
    }
