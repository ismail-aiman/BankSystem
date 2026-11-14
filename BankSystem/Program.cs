namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new BankDBContext();
            while (true) 
            {
                Console.WriteLine("===========================");
                Console.WriteLine("National Bank - Management");
                Console.WriteLine("============================");
                Console.WriteLine("1) Add a new customer");
                Console.WriteLine("2) Open a new account for a austomer");
                Console.WriteLine("3) Update account status (Active / Closed)");
                Console.WriteLine("4) Remove an account from a customer");
                Console.WriteLine("5) List all customers (with accounts)");
                Console.WriteLine("0) Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Enter choice: ");
                var choice = Console.ReadLine();
                if (choice == "0") { break; }
                switch (choice)
                {
                    case "1": Operations.AddCustomer(context); break;
                    case "2": Operations.OpenAccount(context); break;
                    case "3": Operations.UpdateAccountStatus(context); break;
                    case "4": Operations.RemoveAccount(context); break;
                    case "5": Operations.ListCustomers(context); break;
                    default: Console.WriteLine("Invalid choice"); break;
                }
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
