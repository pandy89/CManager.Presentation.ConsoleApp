using CManager.Domain.Models;
using CManager.Infrastructure.Repos;

namespace CManager.Application.Services;

public class CustomerService : ICustomerService
{

    private readonly ICustomerRepo _customerRepo;

    public CustomerService(ICustomerRepo customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {

        //validering isnullorempty(firstNAme) 

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
            // här kommer throw hamna från customerrepo - getallcustomers
            hasError = true;
            return [];

        }




    }
}

