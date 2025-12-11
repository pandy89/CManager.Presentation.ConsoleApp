using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController
{
    private readonly ICustomerService _customerService;

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void ShowMenu()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ Customer Managagement System ------");
            Console.WriteLine("");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("0. Quit Application");
            Console.WriteLine("");
            Console.Write("Choose option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "0":
                    QuitApplicationDialog();
                    return;

                default:
                    InvalidOptionDialog("Invalid option. Press any key to continue.");
                    break;
            }
        }
    }

    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("------ Create Customer ------");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("StreetAddress", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer was successfully created.");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }
        InvalidOptionDialog("Press any key to continue.");
    }

    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("------ All Customers ------");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine();
            }
        }
        InvalidOptionDialog("Press any key to continue.");
    }

    public void QuitApplicationDialog()
    {
        Console.Clear();
        Console.WriteLine("------- Quit application -------\n");
        Console.Write("Are you sure you want to exit the appliction? (y/n): ");
        var option = Console.ReadLine()!;

        if (option.ToLower() == "y")
            Environment.Exit(0);
    }

    private void InvalidOptionDialog(string message)
    {
        Console.WriteLine("");
        Console.WriteLine(message);
        Console.ReadKey();
    }
}