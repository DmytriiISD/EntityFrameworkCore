using Multithreading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");
var config = builder.Build();
string? connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;

using (ApplicationContext db = new ApplicationContext(options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

//HasData
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var result = from customer in db.Customer
                 join orderedbook in db.OrderedBook on customer.CustomerPhone equals orderedbook.CustomerPhone
                 select new
                 {
                     customer.CustomerPhone,
                     customer.CustomerFullName,
                     orderedbook.ISBN,
                     orderedbook.Title
                 };
    foreach (var r in result)
        Console.WriteLine($"CustomerPhone:{r.CustomerPhone} CustomerFullName:{r.CustomerFullName} " +
            $"ISBN:{r.ISBN} Title:{r.Title}");
}*/

//HasData2
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var customers = await db.Customer.ToListAsync();
    foreach (Customer c in customers)
        Console.WriteLine($"CustomerPhone:{c.CustomerPhone} CustomerFullName:{c.CustomerFullName}" +
            $" ISBN:{c.OrderedBook.FirstOrDefault()?.ISBN} Title:{c.OrderedBook.FirstOrDefault()?.Title}");
}*/

//lock write
/*using (ApplicationContext db = new ApplicationContext(options))
{
    List<Customer> customers = new List<Customer>();
    List<OrderedBook> orderedbooks = new List<OrderedBook>();
    object theLocker = new();
    uint counter = 1;

    ThreadStart actions = () =>
    {
        for (uint i = 0; i < 100; i++)
        {
            lock (theLocker)
            {
                customers.Add(new Customer { CustomerPhone = $"PhoneNumber#{counter}", CustomerFullName = "SomeFullName" });
                orderedbooks.Add(new OrderedBook { ISBN = $"ISBN#{counter}", Title = "SomeTitle" });
                counter++;
            }
        }
    };

    Thread thread1 = new Thread(actions);
    Thread thread2 = new Thread(actions);

    thread1.Start();
    thread2.Start();

    Console.Write("");
    Console.WriteLine($"Result: {counter}");
    Console.WriteLine($"Thread1 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread1 state: {thread1.ThreadState}");
    Console.WriteLine($"Thread2 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread2 state: {thread2.ThreadState}");

    await db.Customer.AddRangeAsync(customers);
    await db.OrderedBook.AddRangeAsync(orderedbooks);

    await db.SaveChangesAsync();
}*/

//monitor write
/*using (ApplicationContext db = new ApplicationContext(options))
{
    List<Customer> customers = new List<Customer>();
    List<OrderedBook> orderedbooks = new List<OrderedBook>();
    object theLocker = new();
    uint counter = 1;

    ThreadStart actions = () =>
    {
        for (uint i = 0; i < 100; i++)
        {
            Monitor.Enter(theLocker);
            try
            {
                customers.Add(new Customer { CustomerPhone = $"PhoneNumber#{counter}", CustomerFullName = "SomeFullName" });
                orderedbooks.Add(new OrderedBook { ISBN = $"ISBN#{counter}", Title = "SomeTitle" });
                counter++;
            }
            finally
            {
                Monitor.Exit(theLocker);
            }
        }
    };

    Thread thread1 = new Thread(actions);
    Thread thread2 = new Thread(actions);

    thread1.Start();
    thread2.Start();

    Console.Write("");
    Console.WriteLine($"Result: {counter}");
    Console.WriteLine($"Thread1 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread1 state: {thread1.ThreadState}");
    Console.WriteLine($"Thread2 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread2 state: {thread2.ThreadState}");

    await db.Customer.AddRangeAsync(customers);
    await db.OrderedBook.AddRangeAsync(orderedbooks);

    await db.SaveChangesAsync();
}*/

//lock read
/*using (ApplicationContext db = new ApplicationContext(options))
{
    object theLocker = new();
    var customers = await db.Customer.ToListAsync();

    ThreadStart actions = () =>
    {
        lock (theLocker)
        {
            customers.ForEach(x => Console.Write(x.CustomerPhone));
        }
    };

    Thread thread1 = new Thread(actions);
    Thread thread2 = new Thread(actions);

    thread1.Start();
    thread2.Start();

    Console.WriteLine($"Thread1 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread1 state: {thread1.ThreadState}");
    Console.WriteLine($"Thread2 working?: {thread1.IsAlive}");
    Console.WriteLine($"Thread2 state: {thread2.ThreadState}");
}*/

//Task
/*using (ApplicationContext db = new ApplicationContext(options))
{
    List<Customer> customers = new List<Customer>();
    List<OrderedBook> orderedbooks = new List<OrderedBook>();

    Task task1 = new Task(() =>
    {
        for (uint i = 0; i < 100; i++)
            customers.Add(new Customer { CustomerPhone = $"PhoneNumber#{i}", CustomerFullName = "SomeFullName" });
    });

    task1.Start();

    Task task2 = new Task(() =>
    {
        for (uint i = 0; i < 100; i++)
            orderedbooks.Add(new OrderedBook { ISBN = $"ISBN#{i}", Title = "SomeTitle" });
    });

    Console.WriteLine("Before Wait()");
    Console.WriteLine($"Task1 status: {task1.Status}");
    Console.WriteLine($"Task2 status: {task2.Status}");

    task2.Start();

    task1.Wait();
    task2.Wait();

    Console.WriteLine("\nAfter Wait()");
    Console.WriteLine($"Task1 status: {task1.Status}");
    Console.WriteLine($"Task2 status: {task2.Status}");

    await db.Customer.AddRangeAsync(customers);
    await db.OrderedBook.AddRangeAsync(orderedbooks);

    await db.SaveChangesAsync();
}*/

//SomeFunc
{
    SomeFunc sf = new SomeFunc(options);
    Stopwatch time1 = new Stopwatch();
    Stopwatch time2 = new Stopwatch();
    Stopwatch time3 = new Stopwatch();

    time1.Start();
    sf.AddTasks(1000);
    time1.Stop();

    time2.Start();
    sf.AddMassTasks(1000);
    time2.Stop();

    time3.Start();
    sf.AddThreads(1000);
    time3.Stop();

    Console.WriteLine($"Time taken to complete the AddTasks operation: {time1.ElapsedMilliseconds}ms");
    Console.WriteLine($"Time taken to complete the AddMassTasks operation: {time2.ElapsedMilliseconds}ms");
    Console.WriteLine($"Time taken to complete the AddThreads operation: {time3.ElapsedMilliseconds}ms");
}