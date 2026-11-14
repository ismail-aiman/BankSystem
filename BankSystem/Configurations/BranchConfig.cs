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
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(new Branch { Id = 1, Name = "Main Branch", Address = "cairo",Number="01234",ManagerId=1 },
                            new Branch { Id = 2, Name = "Second Branch", Address = "alex",Number="01235",ManagerId=2 });
        }
    }
}
