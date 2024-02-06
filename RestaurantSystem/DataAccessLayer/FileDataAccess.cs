using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestaurantSystem.DataAccessLayer.Interfaces;

namespace RestaurantSystem.DataAccessLayer
    {
    internal class FileDataAccess<T> : IFileDataAccess<T>
        {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
            {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
            };
        public IEnumerable<T> ReadAll(string filePath)
            {
            if (!File.Exists(filePath))
                {
                return new List<T>();
                }
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<IEnumerable<T>>(json, JsonSerializerOptions) ?? new List<T>();
            }

        public void WriteAll(string filePath, IEnumerable<T> records)
            {

            string json = JsonSerializer.Serialize(records, JsonSerializerOptions);
            File.WriteAllText(filePath, json);
            }
        }
    }
