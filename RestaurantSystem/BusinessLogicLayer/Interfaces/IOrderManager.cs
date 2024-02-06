using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer.Interfaces
    {
    public interface IOrderManager
        {
        Order CreateOrder(int tableNumber, List<OrderItem> items);
        }
    }
