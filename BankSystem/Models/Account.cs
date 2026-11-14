using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal CurrentBalance { get; set; }
        public string AccountType { get; set; }
        public DateTime OpeningDate { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<AccountCustomer> CustomerAccounts { get; set; }
    }
}
