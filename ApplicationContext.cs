using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace EFC
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Book => Set<Book>();
        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Edition> Edition => Set<Edition>();
        public DbSet<ListOfGoodsInTheOrder> ListOfGoodsInTheOrder => Set<ListOfGoodsInTheOrder>();
        public DbSet<Order> Order => Set<Order>();
        public DbSet<Payment> Payment => Set<Payment>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(BookConfigure);
            modelBuilder.Entity<Customer>(CustomerConfigure);
            modelBuilder.Entity<Edition>(EditionConfigure);
            modelBuilder.Entity<ListOfGoodsInTheOrder>(ListOfGoodsInTheOrderConfigure);
            modelBuilder.Entity<Order>(OrderConfigure);
            modelBuilder.Entity<Payment>(PaymentConfigure);
        }

        public void BookConfigure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.ISBN).HasName("PK_Book_ISBN");
            //builder.HasIndex(b => b.EditionNumber).IsUnique(false);

            builder.Property(b => b.NumberOfBooksInStock).HasDefaultValue(30);

            builder.ToTable(b => b.HasCheckConstraint("NumberOfPages", "NumberOfPages > 0"));
            builder.ToTable(b => b.HasCheckConstraint("NumberOfBooksInStock", "YearOfPrinting BETWEEN 1950 AND 2022"));
            builder.ToTable(b => b.HasCheckConstraint("NumberOfBooksInStock", "NumberOfBooksInStock >= 0"));

            builder.HasMany(b => b.Edition).WithOne(b => b.Book);//.HasForeignKey(b => b.EditionNumber);

            builder.HasData(
                new { ISBN = "9788832530391", EditionNumber = "27326", Title = "The Story of Doctor Dolittle", AuthorsName = "Hugh Lofting", Genre = "Fantasy", NumberOfPages = 169, YearOfPrinting = 2019, ActualPrice = 2.29 },
                new { ISBN = "9783748118794", EditionNumber = "15218", Title = "The Adventures of Robin Hood", AuthorsName = "Howard Pyle", Genre = "Adventure", NumberOfPages = 371, YearOfPrinting = 2018, ActualPrice = 5.39 },
                new { ISBN = "9781848703520", EditionNumber = "80115", Title = "The Picture of Dorian Gray", AuthorsName = "Oscar Wilde", Genre = "Novel", NumberOfPages = 348, YearOfPrinting = 2011, ActualPrice = 3.79 },
                new { ISBN = "9781442457492", EditionNumber = "54106", Title = "The Adventures of Tom Sawyer", AuthorsName = "Mark Twain", Genre = "Adventure", NumberOfPages = 289, YearOfPrinting = 2012, ActualPrice = 4.99 },
                new { ISBN = "9781848703483", EditionNumber = "93323", Title = "The Count of Monte Cristo", AuthorsName = "Alexandre Dumas", Genre = "Adventure novel", NumberOfPages = 1803, YearOfPrinting = 2011, ActualPrice = 19.99 },
                new { ISBN = "9781416501831", EditionNumber = "61827", Title = "Frankenstein", AuthorsName = "Mary Shelley", Genre = "Horror", NumberOfPages = 339, YearOfPrinting = 2004, ActualPrice = 7.19 },
                new { ISBN = "9781607108337", EditionNumber = "90300", Title = "Dracula", AuthorsName = "Bram Stoker", Genre = "Horror", NumberOfPages = 524, YearOfPrinting = 2012, ActualPrice = 9.99 },
                new { ISBN = "9781443425292", EditionNumber = "13396", Title = "Gulliver Travels", AuthorsName = "Jonathan Swift", Genre = "Fantasy", NumberOfPages = 359, YearOfPrinting = 2013, ActualPrice = 6.49 },
                new { ISBN = "9788834161661", EditionNumber = "75903", Title = "The Hound of the Baskervilles", AuthorsName = "Arthur Conan Doyle", Genre = "Detective", NumberOfPages = 212, YearOfPrinting = 2019, ActualPrice = 5.89 },
                new { ISBN = "9780547249643", EditionNumber = "28642", Title = "1984", AuthorsName = "George Orwell", Genre = "Dystopie", NumberOfPages = 382, YearOfPrinting = 1983, ActualPrice = 7.99 }
                );
        }

        public void CustomerConfigure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerPhone).HasName("PK_Customer_CustomerPhone");

            builder.HasAlternateKey(c => c.Email);

            builder.ToTable(c => c.HasCheckConstraint("CustomerFullName", "CustomerFullName != ''"));
            builder.ToTable(c => c.HasCheckConstraint("Email", "Email != ''"));

            builder.HasData(
                new { CustomerPhone = "+380670835294", CustomerFullName = "Paul Clark", DateOfBirth = new DateTime(1971, 08, 27), Email = "kaft93x@outlook.com" },
                new { CustomerPhone = "+380967220511", CustomerFullName = "Patricia Montgomery", DateOfBirth = new DateTime(1977, 11, 16), Email = "715qy08@gmail.com" },
                new { CustomerPhone = "+380972103055", CustomerFullName = "Douglas Robinson", DateOfBirth = new DateTime(1992, 04, 11), Email = "q4as80@outlook.com" },
                new { CustomerPhone = "+380976943805", CustomerFullName = "Joseph Garcia", DateOfBirth = new DateTime(1999, 06, 07), Email = "i8ovxn2f@gmail.com" },
                new { CustomerPhone = "+380985502296", CustomerFullName = "Linda Wilson", DateOfBirth = new DateTime(1984, 08, 10), Email = "dihf8jxk@gmail.com" }
                );
        }

        public void EditionConfigure(EntityTypeBuilder<Edition> builder)
        {
            builder.HasKey(e => e.EditionNumber).HasName("PK_Edition_EditionNumber");

            builder.Property(e => e.IssueNumber).HasDefaultValue(1);
            builder.Property(e => e.Circulation).HasDefaultValue(1000);

            builder.HasData(
                new { EditionNumber = "27326", PublishingHouse = "Abela Publishing" },
                new { EditionNumber = "15218", PublishingHouse = "Books on Demand" },
                new { EditionNumber = "80115", PublishingHouse = "Wordsworth Editions" },
                new { EditionNumber = "54106", PublishingHouse = "Aladdin" },
                new { EditionNumber = "93323", PublishingHouse = "Wordsworth Editions" },
                new { EditionNumber = "61827", PublishingHouse = "Pocket Books" },
                new { EditionNumber = "90300", PublishingHouse = "Canterbury Classics" },
                new { EditionNumber = "13396", PublishingHouse = "HarperCollins" },
                new { EditionNumber = "75903", PublishingHouse = "Diamond Book Publishing" },
                new { EditionNumber = "28642", PublishingHouse = "Mariner Books" }
                );
        }

        public void ListOfGoodsInTheOrderConfigure(EntityTypeBuilder<ListOfGoodsInTheOrder> builder)
        {
            builder.HasKey(l => l.ProductNumberInTheOrder).HasName("PK_ListOfGoodsInTheOrder_ProductNumberInTheOrder");
            //builder.HasIndex(l => l.ISBN).IsUnique(false);
            //builder.HasIndex(l => l.OrderNumber).IsUnique(false);

            builder.Property(l => l.NumberOfBooksOrdered).HasDefaultValue(1);

            builder.ToTable(l => l.HasCheckConstraint("NumberOfBooksOrdered", "NumberOfBooksOrdered >= 0"));
            builder.ToTable(l => l.HasCheckConstraint("OrderedPrice", "OrderedPrice >= 0"));

            builder.HasOne(l => l.Book).WithMany(l => l.ListOfGoodsInTheOrder).HasForeignKey(l => l.ISBN);
            builder.HasOne(l => l.Order).WithMany(l => l.ListOfGoodsInTheOrder).HasForeignKey(l => l.OrderNumber);

            builder.HasData(
                new { ProductNumberInTheOrder = 1, OrderNumber = 1, ISBN = "9783748118794", NumberOfBooksOrdered = 2, OrderedPrice = 5.39 },
                new { ProductNumberInTheOrder = 2, OrderNumber = 1, ISBN = "9781442457492", NumberOfBooksOrdered = 3, OrderedPrice = 4.99 },
                new { ProductNumberInTheOrder = 3, OrderNumber = 2, ISBN = "9781416501831", NumberOfBooksOrdered = 1, OrderedPrice = 7.19 },
                new { ProductNumberInTheOrder = 4, OrderNumber = 3, ISBN = "9781443425292", NumberOfBooksOrdered = 1, OrderedPrice = 6.49 },
                new { ProductNumberInTheOrder = 5, OrderNumber = 3, ISBN = "9780547249643", NumberOfBooksOrdered = 1, OrderedPrice = 7.99 },
                new { ProductNumberInTheOrder = 6, OrderNumber = 3, ISBN = "9788832530391", NumberOfBooksOrdered = 2, OrderedPrice = 2.29 },
                new { ProductNumberInTheOrder = 7, OrderNumber = 4, ISBN = "9781848703520", NumberOfBooksOrdered = 4, OrderedPrice = 3.79 },
                new { ProductNumberInTheOrder = 8, OrderNumber = 4, ISBN = "9781848703483", NumberOfBooksOrdered = 1, OrderedPrice = 19.99 },
                new { ProductNumberInTheOrder = 9, OrderNumber = 5, ISBN = "9781443425292", NumberOfBooksOrdered = 20, OrderedPrice = 6.49 },
                new { ProductNumberInTheOrder = 10, OrderNumber = 5, ISBN = "9781607108337", NumberOfBooksOrdered = 1, OrderedPrice = 9.99 },
                new { ProductNumberInTheOrder = 11, OrderNumber = 6, ISBN = "9788834161661", NumberOfBooksOrdered = 3, OrderedPrice = 5.89 },
                new { ProductNumberInTheOrder = 12, OrderNumber = 6, ISBN = "9780547249643", NumberOfBooksOrdered = 1, OrderedPrice = 7.99 }
                );
        }

        public void OrderConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderNumber).HasName("PK_Order_OrderNumber");
            //builder.HasIndex(o => o.CustomerPhone).IsUnique(false);
            //builder.HasIndex(o => o.PaymentNumber).IsUnique(false);

            builder.Property(o => o.OrderDate).HasDefaultValue(DateTime.Today);
            builder.Property(o => o.OrderStatus).HasDefaultValue(true);
            builder.Property(o => o.PaymentMethod).HasDefaultValue("Cash");
            builder.Property(o => o.DeliveryMethod).HasDefaultValue("Pickup");
            builder.Property(o => o.DeliveryService).HasDefaultValue("-");
            builder.Property(o => o.DeliveryAddress).HasDefaultValue("-");

            builder.ToTable(o => o.HasCheckConstraint("Cost", "Cost >= 0"));

            builder.HasOne(o => o.Customer).WithMany(o => o.Order).HasForeignKey(o => o.CustomerPhone);
            builder.HasOne(o => o.Payment).WithMany(o => o.Order).HasForeignKey(o => o.PaymentNumber);

            builder.HasData(
                new { OrderNumber = 1, Cost = 25.75, CustomerPhone = "+380670835294", RecipientsPhone = "+380670835294", RecipientFullName = "Paul Clark", PaymentNumber = 1, OrderDate = new DateTime(2022, 03, 14) },
                new { OrderNumber = 2, Cost = 7.19, CustomerPhone = "+380967220511", RecipientsPhone = "+380967220511", RecipientFullName = "Patricia Montgomery", PaymentNumber = 2, OrderDate = new DateTime(2022, 01, 10) },
                new { OrderNumber = 3, Cost = 19.06, CustomerPhone = "+380972103055", RecipientsPhone = "+380972103055", RecipientFullName = "Douglas Robinson", PaymentNumber = 3, OrderDate = new DateTime(2022, 02, 03) },
                new { OrderNumber = 4, Cost = 35.15, CustomerPhone = "+380976943805", RecipientsPhone = "+380976943805", RecipientFullName = "Joseph Garcia", PaymentNumber = 4, OrderDate = new DateTime(2022, 04, 28) },
                new { OrderNumber = 5, Cost = 139.79, CustomerPhone = "+380985502296", RecipientsPhone = "+380985502296", RecipientFullName = "Linda Wilson", PaymentNumber = 5, OrderDate = new DateTime(2022, 05, 17) },
                new { OrderNumber = 6, Cost = 25.66, CustomerPhone = "+380967220511", RecipientsPhone = "+380968712909", RecipientFullName = "Antonio Ramirez", PaymentNumber = 6, OrderDate = new DateTime(2022, 01, 20), OrderStatus = false, PaymentMethod = "Card", DeliveryMethod = "Mail", DeliveryService = "Nova Poshta", DeliveryAddress = "Ukraine, Kyiv, Khreshchatyk 21, app.22" }
                );
        }

        public void PaymentConfigure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentNumber).HasName("PK_Payment_PaymentNumber");

            builder.ToTable(p => p.HasCheckConstraint("ActualAmount", "ActualAmount >= 0"));

            builder.HasData(
                new { PaymentNumber = 1, ActualAmount = 25.75 },
                new { PaymentNumber = 2, ActualAmount = 7.19 },
                new { PaymentNumber = 3, ActualAmount = 19.06 },
                new { PaymentNumber = 4, ActualAmount = 35.15 },
                new { PaymentNumber = 5, ActualAmount = 139.79 },
                new { PaymentNumber = 6, ActualAmount = 0.0 }
                );
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