using HajjProduct.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HajjProduct.Infrastructure;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // how to register your entities
    // Ex: public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }


}
