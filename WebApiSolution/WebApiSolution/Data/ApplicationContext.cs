using Microsoft.EntityFrameworkCore;

namespace WebApiSolution.Data;

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}