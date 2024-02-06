using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantSystem.DataAccessLayer.Interfaces
    {
    public interface IFileDataAccess<T>
        {
        IEnumerable<T> ReadAll(string filePath);
        void WriteAll(string filePath, IEnumerable<T> records);
        }
    }
