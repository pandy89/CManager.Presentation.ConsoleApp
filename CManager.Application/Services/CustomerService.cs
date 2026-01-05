using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using System.Diagnostics;

namespace CManager.Application.Services;

public class CustomerService(ICustomerRepo customerRepo): ICustomerService
{
    private readonly ICustomerRepo _customerRepo = customerRepo;

    //What the customer must have to been created. 
    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    //
    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            hasError = true;
            return [];
        }
    }

    //What the 
    public bool DeleteCustomer(Guid id)
    {
        if (id == Guid.Empty)
            return false;

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id); // Lamda expression

            //Information of Lamda Expression.

            //foreach (var c in customer)
            //if (c.id == id) return c
            //return null

            if (customer == null)
                return false;

            customers.Remove(customer);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception ex)
        {
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
                return false;
            }
        }
    }

    
    // gets a single customer from the list by email address 
    // <returns>returns a single contact if the contact exists, returns null if else</returns>
    public CustomerModel GetSingleCustomer(string email)
    {
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(e => e.Email == email);
            return customer ??= null!;
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine("" + ex.Message); 
        }
        return null!;
    }
}
