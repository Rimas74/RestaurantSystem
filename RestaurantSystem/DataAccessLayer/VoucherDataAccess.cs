using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.Models;


namespace RestaurantSystem.DataAccessLayer
    {
    public class VoucherDataAccess
        {
        private readonly FileDataAccess<RestaurantVoucher> _fileDataAccess;
        private const string FilePath = "DataFiles/RestaurantVouchers/";
        public VoucherDataAccess()
            {
            _fileDataAccess = new FileDataAccess<RestaurantVoucher>();
            }

        public void SaveRestaurantVoucher(RestaurantVoucher voucher)
            {


            if (!Directory.Exists(FilePath))
                {
                Directory.CreateDirectory(FilePath);
                }

            try
                {
                string fullFilePath = $"{FilePath}Voucher_{voucher.OrderId}.json";
                Console.WriteLine($"Saving voucher to: {fullFilePath}");
                _fileDataAccess.WriteAll(fullFilePath, new List<RestaurantVoucher> { voucher });
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error saving file {ex.Message}");
                }


            }
        }
    }
