using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Configurations
{
    public class AccountCustomerConfig : IEntityTypeConfiguration<AccountCustomer>
    {
        public void Configure(EntityTypeBuilder<AccountCustomer> builder)
        {
            builder.HasKey(ac => new { ac.AccountId, ac.CustomerId });
        }
    }
}
