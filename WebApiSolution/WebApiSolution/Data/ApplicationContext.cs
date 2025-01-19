using Microsoft.EntityFrameworkCore;
using WebApiSolution.Models;

namespace WebApiSolution.Data;

public sealed class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Account> Accounts => Set<Account>();
}