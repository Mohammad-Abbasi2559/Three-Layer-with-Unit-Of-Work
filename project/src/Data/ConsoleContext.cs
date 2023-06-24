using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;
public class ConsoleContext:DbContext
{
    public ConsoleContext(): base() { }

    protected override void OnConfiguring(DbContextOptionsBuilder builder){
        builder.UseSqlServer(@"Server=(local);Database=UnitOfWorkTest;Trusted_Connection=True;TrustServerCertificate=True");
    }

    public DbSet<Person> People { get; set; }
}
