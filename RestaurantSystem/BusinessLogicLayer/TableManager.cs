using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.DataAccessLayer;
using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer
    {
    public class TableManager : ITableManager
        {
        private readonly TableDataAccess _tableDataAccess;
        private List<Table> _tables;

        public TableManager(TableDataAccess tableDataAccess)
            {
            _tableDataAccess = tableDataAccess;
            LoadTables();
            }

        private void LoadTables()
            {
            _tables = _tableDataAccess.GetAllTables().ToList();
            }

        public bool OccupyTable(int tableNumber)
            {
            var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table != null && !table.IsOccupied)
                {
                table.IsOccupied = true;
                _tableDataAccess.SaveTables(_tables);
                return true;
                }

            return false;
            }

        public bool VacateTable(int tableNumber)
            {
            var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table != null && table.IsOccupied)
                {
                table.IsOccupied = false;
                _tableDataAccess.SaveTables(_tables);
                return true;
                }

            return false;
            }

        public IEnumerable<Table> GetAllTables()
            {
            return _tables;
            }
        }

    }
