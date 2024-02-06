using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.Models;


namespace RestaurantSystem.DataAccessLayer
    {
    public class TableDataAccess
        {
        private readonly FileDataAccess<Table> _fileDataAccess;
        private const string FilePath = "DataFiles//tables.json";

        public TableDataAccess()
            {
            _fileDataAccess = new FileDataAccess<Table>();
            }

        public IEnumerable<Table> GetAllTables()
            {
            return _fileDataAccess.ReadAll(FilePath);
            }

        public void SaveTables(IEnumerable<Table> tables)
            {
            _fileDataAccess.WriteAll(FilePath, tables);
            }
        }
    }
