using api3teste.Entity;
using Microsoft.EntityFrameworkCore;


namespace api3teste.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        { 
        }

        public DbSet<Car> Car {  get; set; }
    }
}
