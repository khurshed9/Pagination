using DTOWithSRM.Entities;
using Microsoft.EntityFrameworkCore;

namespace DTOWithSRM;

public class PersonDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    
    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
    {
    }
    
}