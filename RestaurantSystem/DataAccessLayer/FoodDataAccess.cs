using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.DataAccessLayer.Interfaces;
using RestaurantSystem.Models;

namespace RestaurantSystem.DataAccessLayer
    {
    public class FoodDataAccess
        {
        private readonly FileDataAccess<Product> _fileDataAccess;
        private const string FilePath = "DataFiles//food.json";

        public FoodDataAccess()
            {
            _fileDataAccess = new FileDataAccess<Product>();
            }

        public IEnumerable<Product> GetAllFoodItems()
            {
            return _fileDataAccess.ReadAll(FilePath);
            }

        public void SaveFoodItems(IEnumerable<Product> foodItems)
            {
            _fileDataAccess.WriteAll(FilePath, foodItems);
            }

        }

    }
