using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Operations
    {
        public static string ReadText(string str) 
        {

            while (true)
            {
                Console.Write(str);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                Console.WriteLine("You must enter a value");
            }
        }
        public static string ReadDigits(string str)
        {
            while (true)
            {
                Console.Write(str);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.All(char.IsDigit))
                {
                    return input;
                }
                Console.WriteLine("You can insert numbers only");
            }
        }
        public static DateTime ReadDate(string str)
        {
            while (true)
            {
                Console.Write(str);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }
                Console.WriteLine("You must enter a valid date");
            }
        }
        public static int ReadInt(string str)
        {
            while (true)
            {
                Console.Write(str);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int number))
                {
                    return number;
                }
                Console.WriteLine("You must enter a valid number");
            }
        }
        public static void AddCustomer(BankDBContext context) 
        {
            Console.WriteLine("Add new Customer");
            var name = ReadText("Full Name: ");
            var nationalId = ReadDigits("National ID: ");
            var dob = ReadDate("Date of Birth (yyyy-MM-dd): ");
            var email = ReadText("Email: ");
            var address = ReadText("Address: ");
            var number = ReadDigits("Phone Number: ");
            Console.WriteLine("Type 1 for Individual, 2 for Business");
            var typeChoice = ReadInt("choice: ");
            while(typeChoice != 1 && typeChoice != 2)
            {
                Console.WriteLine("CHoose between 1 and 2");
                typeChoice = ReadInt("choice: ");
            }
            var type = typeChoice == 1 ? "Individual" : "Business";
            var customer = new Customer
            {
                FullName = name,
                NationalId = nationalId,
                DateOfBirth = dob,
                Email = email,
                Address = address,
                Number = number,
                CustomerType = type
            };
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        public static void OpenAccount(BankDBContext context)
        {
            Console.WriteLine("Open new Account");
            var accountId = ReadInt("Account ID: ");
            Console.WriteLine("Type 1 for Savings, 2 for Current, 3 fro Business");
            var typeChoice = ReadInt("choice: ");
            while (typeChoice < 1 || typeChoice > 3)
            {
                Console.WriteLine("Choose between 1 ,2, 3");
                typeChoice = ReadInt("choice: ");
            }
            string type = "Savings";
            if (typeChoice == 2)
            {
                type = "Current";
            }
            if (typeChoice == 3)
            {
                type = "Business";
            }
            var branchId = ReadInt("Branch ID: ");
            var branch = context.Branches.Find(branchId);
            if (branch == null)
            {
                Console.WriteLine("Branch not found");
                return;
            }
            var customerId = ReadInt("Customer ID: ");
            var customer = context.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            Console.WriteLine("Ownership Type: 1 for primary, 2 for CoHolder");
            var roleChoice = ReadInt("choice: ");
            while (roleChoice != 1 && roleChoice != 2)
            {
                Console.WriteLine("Choose between 1 and 2");
                roleChoice = ReadInt("choice: ");
            }
            string role;
            if (roleChoice == 1) { role = "Primary"; }
            else { role = "CoHolder"; }
            var account = new Account
            {
                Id = accountId,
                AccountType = type,
                CurrentBalance = 0,
                BranchId = branchId,
                OpeningDate = DateTime.Now,
            };
            context.Accounts.Add(account);
            context.SaveChanges();
            var link = new AccountCustomer
            {
                AccountId = accountId,
                CustomerId = customerId,
                OwnerShipType = role,
                OwnerShipStartDate = DateTime.Now,
                AccountStatus = "Active"
            };
            context.Set<AccountCustomer>().Add(link);
            context.SaveChanges();
        }
        public static void UpdateAccountStatus(BankDBContext context) 
        {
            Console.WriteLine("Update Account Status");
            var accountId = ReadInt("Account ID: ");
            var customerId = ReadInt("Customer ID: ");
            var link = context.Set<AccountCustomer>().Find(accountId, customerId);
            if (link == null)
            {
                Console.WriteLine("Account-Customer link not found");
                return;
            }
            link.AccountStatus= "Inactive";
            context.SaveChanges();
        }
        public static void RemoveAccount(BankDBContext context) 
        {
            Console.WriteLine("Remove Account");
            var accountId = ReadInt("Account ID: ");
            var customerId = ReadInt("Customer ID: ");
            var link = context.Set<AccountCustomer>().Find(accountId, customerId);
            if (link == null)
            {
                Console.WriteLine("Account-Customer link not found");
                return;
            }
            context.Set<AccountCustomer>().Remove(link);
            context.SaveChanges();

        }
        public static void ListCustomers(BankDBContext context)
        {
            var customers = context.Customers.Include(c => c.CustomerAccounts)
                                                .ThenInclude(ca=>ca.Account)
                                                .ThenInclude(a=>a.Branch).ToList();
            foreach (var c in customers)
            {
                Console.WriteLine($"Customer: {c.FullName}, Email: {c.Email},Type: {c.CustomerType}");
                if (!c.CustomerAccounts.Any()) { Console.WriteLine("No accounts found for this customer."); continue; }
                foreach (var ca in c.CustomerAccounts)
                {
                    Console.WriteLine($"AccountId: {ca.AccountId}, AccountType: {ca.Account.AccountType},OwnerShip: {ca.OwnerShipType}");
                }

            }
        }
    }
}
