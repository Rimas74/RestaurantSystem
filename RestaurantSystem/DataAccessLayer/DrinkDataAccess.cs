using RestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DataAccessLayer
    {
    internal class DrinkDataAccess
        {
        private readonly FileDataAccess<Product> _fileDataAccess;
        private const string FilePath = "DataFiles//drinks.json";

        public DrinkDataAccess()
            {
            _fileDataAccess = new FileDataAccess<Product>();
            }

        public IEnumerable<Product> GetAllDrinkItems()
            {
            return _fileDataAccess.ReadAll(FilePath);
            }

        public void SaveDrinkItems(IEnumerable<Product> drinkItems)
            {
            _fileDataAccess.WriteAll(FilePath, drinkItems);
            }
        }
    }
