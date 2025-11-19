using System.Text.Json;
using E_Invoice.Application.Interfaces.IProviders;

namespace E_Invoice.Infrastructure.Providers
{
    public class JsonDataProvider<T> : IJsonDataProvider<T>
    {
        private readonly List<T> _values;

        public JsonDataProvider(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException($"JSON file not found at path: {jsonFilePath}");

            var json = File.ReadAllText(jsonFilePath);

           
            _values = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public IEnumerable<T> GetAll() => _values;
    }
}
