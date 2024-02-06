using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer.Interfaces
    {
    public interface ITableManager
        {
        bool OccupyTable(int tableNumber);
        bool VacateTable(int tableNumber);
        IEnumerable<Table> GetAllTables();
        }
    }
