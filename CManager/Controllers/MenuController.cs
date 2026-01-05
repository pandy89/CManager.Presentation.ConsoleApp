using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    //Main menu of the application 
    public void ShowMenu()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ Customer Managagement System ------");
            Console.WriteLine("");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. Delete Customer");
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

                case "3":
                    DeleteCustomer();
                    break;

                case "0":
                    QuitApplicationDialog();
                    return;

                default:
                    InvalidOptionDialog("Invalid option! Press any key to continue.");
                    break;
            }
        }
    }

    //Creating of a customer.
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("------ Create Customer ------");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("");
            Console.WriteLine("Customer was successfully created.");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again");
        }
        Console.WriteLine("___________________________________________");
        InvalidOptionDialog("Press any key to continue.");
    }

    //Display all the customers in a list.
    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("------ All Customers ------");
        Console.WriteLine();

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine();
                Console.WriteLine("___________________________________________");
                Console.WriteLine();
            }

            Console.WriteLine("Please copy the email (ctrl+c) of the customer you wish to view later.");
            Console.WriteLine("");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("");
            Console.WriteLine("1. Add a new customer");
            Console.WriteLine("2. View a customer details");
            Console.WriteLine("3. Delete a customer");
            Console.WriteLine("4. Back to main menu");
            Console.WriteLine("");
            Console.Write("Enter your option: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;
                case "2":
                    ShowSingleCustomer();
                    break;
                case "3":
                    DeleteCustomer();
                    break;
                case "4":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    ViewAllCustomers();
                    break;
            }

        }
        InvalidOptionDialog("Press any key to continue.");
        
    }

    public void ShowSingleCustomer()
    {
        Console.Clear();
        Console.Write("Enter email address: ");
        var email = Console.ReadLine();
        var customer = _customerService.GetSingleCustomer(email!);
        if (customer != null)
        {
            Console.WriteLine("");
            Console.WriteLine($"First Name: {customer.FirstName}");
            Console.WriteLine($"Last Name: {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone Number: {customer.PhoneNumber}");
            Console.WriteLine($"Streetaddress: {customer.Address.StreetAddress}");
            Console.WriteLine($"Postal Code: {customer.Address.PostalCode}");
            Console.WriteLine($"City: {customer.Address.City}");
            Console.WriteLine("");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("");
            Console.WriteLine("1. Back to main menu");
            Console.WriteLine("2. List all customers");
            Console.WriteLine("3. Delete customer");
            Console.WriteLine("");
            Console.Write("Enter your option: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    ShowMenu();
                    break;
                case "2":
                    ViewAllCustomers();
                    break;
                case "3":
                    DeleteCustomer();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    ShowSingleCustomer();
                    break;
            }
        }
        else
        {
            Console.WriteLine("No customer found with the given email address.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            ShowMenu();
        }
    }

    //Deleting of a customer from a list. 
    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("------ Delete Customer ------");
        Console.WriteLine();

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList(); 
        //ToList is added because we need to convert the IEnumerable list to an editable list in order to change the Guid to an index number for easier removal of a customer. This is done only here and not on all IEnumerable lists.

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
            while (true)
            {
                for (int i = 0; i < customers.Count; i++) // Create the indexing to 0, 1, 2, 3
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1}]{customer.FirstName} {customer.LastName} {customer.Email}");
                    Console.WriteLine("___________________________________________");
                    Console.WriteLine();
                }
                Console.WriteLine("[0] Go Back to menu");
                Console.Write("Enter customer number to delete: ");
                var input = Console.ReadLine();

                //Check that there is a number in our input and that we have entered something in our field.
                if (!int.TryParse(input, out int choice))
                {
                    InvalidOptionDialog("Not a vaild number. Press any key to try again.");
                    continue;
                }

                //Check that the number 0 is selected to go back to the mainmenu.
                if (choice == 0)
                {
                    return;
                }

                //Check if we selected the right customer to delete.
                if (choice > customers.Count) // 0,1,2 => 1,2,3
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again"); // {customers.Count} shows the totalt number of customer.
                    Console.ReadKey();
                    continue;
                }

                //Validting if we choice the right customer by showing the name. 
                var index = choice - 1;
                var selectedCustomer = customers[index];

                Console.WriteLine();
                Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");

                while (true)
                {
                    Console.Write("Are you sure you want to delete this customer? (y/n)");
                    var confirmation = Console.ReadLine()!.ToLower();

                    if (confirmation == "y")
                    {
                        //Deleting the customer after the user enters y. 
                        var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                        if (result)
                        {
                            InvalidOptionDialog("Customer was successfully removed. Press any key to go back.");
                            return;                      
                        }
                        else
                        {
                            InvalidOptionDialog("Something went wrong. Please contact support. Press any key to continue.");
                            return;
                        }
                    }
                    else if (confirmation == "n")
                    {
                        //Return the user´s choice if the user enters n. 
                        return;
                    }
                    else
                    {
                        InvalidOptionDialog("Please enter y for yes or n for no. Press any key to try again.");
                        continue;
                    }
                }
            } 
        }
        InvalidOptionDialog("Press any key to continue.");
            
    }

    private void InvalidOptionDialog(string message)
    {
        Console.WriteLine("");
        Console.WriteLine(message);
        Console.ReadKey();
    }

    private void QuitApplicationDialog()
    {
        Console.Clear();
        Console.WriteLine("------- Quit application -------\n");
        Console.Write("Are you sure you want to exit the appliction? (y/n): ");
        var option = Console.ReadLine()!;

        if (option.ToLower() == "y")
            Environment.Exit(0);
    }
}
