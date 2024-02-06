using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Utilities
    {
    public static class VoucherIdTracker
        {
        private const string LoadIdFilePath = "DataFiles/RestaurantVouchers/LastVoucherId.txt";

        public static int GetNextVoucherId()
            {
            int lastId = ReadLastVoucherId();
            UpdateLastVoucherId(lastId + 1);
            return lastId + 1;
            }

        public static void UpdateLastVoucherId(int newId)
            {
            EnsureDirectoryExists();
            File.WriteAllText(LoadIdFilePath, newId.ToString());
            }

        private static void EnsureDirectoryExists()
            {
            var directoryPath = Path.GetDirectoryName(LoadIdFilePath);
            if (!Directory.Exists(directoryPath))
                {
                Directory.CreateDirectory(directoryPath);
                }
            }

        private static int ReadLastVoucherId()
            {
            if (!File.Exists(LoadIdFilePath))
                {
                return 0;
                }
            string idString = File.ReadAllText(LoadIdFilePath);
            return int.TryParse(idString, out int lastId) ? lastId : 0;
            }
        }
    }
