using System;
using System.Collections.Generic;
using System.Text;
using DrugStoreManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrugStoreManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Drug> Drugs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
