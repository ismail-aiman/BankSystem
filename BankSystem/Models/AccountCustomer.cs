using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class AccountCustomer
    {
        public int? AccountId { get; set; }
        public Account? Account { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OwnerShipStartDate { get; set; }
        public string OwnerShipType { get; set; }
        public string AccountStatus { get; set; }

    }
}
