using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;

namespace CManager.Test.ServiceTests; 

public class CustomerServiceTest
{
    [Fact]
    public void DeleteCustomer_WithEmptyGuid_ReturnsFalse()
    {
        //Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>());

        var service = new CustomerService(mockCustomerRepo.Object);

        //Act
        var result = service.DeleteCustomer(Guid.Empty);

        //Assert
        Assert.False(result );
        mockCustomerRepo.Verify(r => r.GetAllCustomers(), Times.Never);

    }

    [Fact]
    public void DeleteCustomer_WhenCustomerExists_ReturnsFalse()
    {
        //Arrange
        var testCustomer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@domain.com",
            PhoneNumber = "1234567890",
            Address = new AddressModel
            {
                StreetAddress = "Street 1",
                City = "City",
                PostalCode = "12345",
            }
        };

        var testCustomers = new List<CustomerModel> { testCustomer };

        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(testCustomers);
        mockCustomerRepo.Setup(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>())).Returns(true);

        var service = new CustomerService(mockCustomerRepo.Object);

        //Act
        var result = service.DeleteCustomer(testCustomer.Id);

        //Assert
        Assert.True(result);
        mockCustomerRepo.Verify(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>()), Times.Once);
    }

    [Fact]
    public void DeleteCustomer_WhenRepositoryReturnsNull_ReturnsFalse()
    {
        //Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns((List<CustomerModel>)null!);

        var service = new CustomerService(mockCustomerRepo.Object);

        //Act
        var result = service.DeleteCustomer(Guid.NewGuid());

        //Assert
        Assert.False(result);
    }
}
