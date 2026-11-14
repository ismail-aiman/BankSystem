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
    public class ManagerConfig : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData(new Manager {Id=1, FullName="ismail ayman", Email="@yahoo.com",Number="0123" },
                            new Manager { Id = 2, FullName = "ahmed ayman", Email = "@gmail.com", Number = "01234" });
        }
    }
}
