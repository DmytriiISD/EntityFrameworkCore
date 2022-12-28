using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Multithreading
{
    public class ApplicationContext : DbContext
    {
        public DbSet<OrderedBook> OrderedBook => Set<OrderedBook>();
        public DbSet<Customer> Customer => Set<Customer>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedBook>(OrderedBookConfigure);
            modelBuilder.Entity<Customer>(CustomerConfigure);
        }

        public void CustomerConfigure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerPhone).HasName("PK_Customer_CustomerPhone");

            /*builder.HasData(
                new { CustomerPhone = "+380670835294", CustomerFullName = "Paul Clark" },
                new { CustomerPhone = "+380967220511", CustomerFullName = "Patricia Montgomery" },
                new { CustomerPhone = "+380972103055", CustomerFullName = "Douglas Robinson" },
                new { CustomerPhone = "+380976943805", CustomerFullName = "Joseph Garcia" },
                new { CustomerPhone = "+380985502296", CustomerFullName = "Linda Wilson" }
                );*/
        }

        public void OrderedBookConfigure(EntityTypeBuilder<OrderedBook> builder)
        {
            builder.HasKey(b => b.ISBN).HasName("PK_OrderedBook_ISBN");

            builder.HasOne(c => c.Customer).WithMany(b => b.OrderedBook).HasForeignKey(c => c.CustomerPhone);

            /*builder.HasData(
                new { ISBN = "9788832530391", Title = "The Story of Doctor Dolittle", CustomerPhone = "+380670835294" },
                new { ISBN = "9783748118794", Title = "The Adventures of Robin Hood", CustomerPhone = "+380967220511" },
                new { ISBN = "9781848703520", Title = "The Picture of Dorian Gray", CustomerPhone = "+380972103055" },
                new { ISBN = "9781442457492", Title = "The Adventures of Tom Sawyer", CustomerPhone = "+380976943805" },
                new { ISBN = "9781848703483", Title = "The Count of Monte Cristo", CustomerPhone = "+380985502296" },
                new { ISBN = "9781416501831", Title = "Frankenstein", CustomerPhone = "+380670835294" },
                new { ISBN = "9781607108337", Title = "Dracula", CustomerPhone = "+380967220511" },
                new { ISBN = "9781443425292", Title = "Gulliver Travels", CustomerPhone = "+380972103055" },
                new { ISBN = "9788834161661", Title = "The Hound of the Baskervilles", CustomerPhone = "+380976943805" },
                new { ISBN = "9780547249643", Title = "1984", CustomerPhone = "+380985502296" }
                );*/
        }
    }

    public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            // getting configuration from appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // getting connection string from appsettings.json file
            string? connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}