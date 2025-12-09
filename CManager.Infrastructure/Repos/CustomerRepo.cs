using CManager.Domain.Models;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;

public class CustomerRepo : ICustomerRepo
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public CustomerRepo(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };
    }

    public List<CustomerModel> GetAllCustomers()
    {
        if(!File.Exists(_filePath))
        {  
            return []; 
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
            return customers ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading customer: {ex.Message}");
            throw;
        }
    }

    public bool SaveCustomers(List<CustomerModel> custromers)
    {
        if (custromers == null)
            return false;

        try
        {
            var json = JsonSerializer.Serialize(custromers, _jsonOptions);

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error saving customer: { ex.Message }");
            return false;
        }
    }
}
