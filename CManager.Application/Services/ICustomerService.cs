using CManager.Domain.Models;

namespace CManager.Application.Services;

//Interface for Create, Read, Update and Deleting customer.
public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    bool DeleteCustomer(Guid id);

    CustomerModel GetSingleCustomer(string email);

}
