using EFC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");
var config = builder.Build();
string? connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;

//Custom function
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var booksbefore = db.Book.ToList();
    Console.WriteLine("Before");
    foreach (Book b in booksbefore)
        Console.WriteLine($"ISBN:{b.ISBN} Title:{b.Title} Actual price:{b.ActualPrice}");

    var book = new BookManagment(options);
    book.EditBookPrice("9781607108337");
    //db.Entry(booksbefore.Find(x => x.ISBN == "9781607108337")).Reload();

    Console.WriteLine();
    Console.WriteLine("After");
    var booksafter = db.Book.ToList();
    foreach (Book b in booksafter)
        Console.WriteLine($"ISBN:{b.ISBN} Title:{b.Title} Actual price:{b.ActualPrice}");
}*/

//Eager loading 1
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var orders = db.Order.Include(l => l.ListOfGoodsInTheOrder)
                         .ThenInclude(b => b!.Book)
                         .ThenInclude(e => e!.Edition)
                         .ToList();
    //foreach (Order o in orders)
    //{
    //    Console.Write($"OrderNumber:{o.OrderNumber} CustomerPhone:{o.CustomerPhone} ");
    //    foreach (ListOfGoodsInTheOrder l in o!.ListOfGoodsInTheOrder)
    //    {
    //        Console.WriteLine();
    //        Console.Write($" NumberOfBooksOrdered:{l.NumberOfBooksOrdered} ");
    //        foreach (Book b in o.ListOfGoodsInTheOrder)
    //            Console.Write($"ISBN:{b.ISBN} ");
    //    }
    //    Console.WriteLine();
    //}
    foreach (Order o in orders)
        Console.WriteLine($"OrderNumber:{o.OrderNumber} CustomerPhone:{o.CustomerPhone}" +
            $" NumberOfBooksOrdered:{o.ListOfGoodsInTheOrder[0].NumberOfBooksOrdered} ISBN:{o.ListOfGoodsInTheOrder[0]?.Book?.ISBN}");
}*/

//Eager loading 2
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var orders = db.Order.Include(l => l.ListOfGoodsInTheOrder)
                         //.Include(b => b.ListOfGoodsInTheOrder[0]!.Book)
                         //.Include(e => e.ListOfGoodsInTheOrder[0]!.Book!.Edition)
                         .ToList();
    foreach (Order o in orders)
        Console.WriteLine($"OrderNumber:{o.OrderNumber} CustomerPhone:{o.CustomerPhone}" +
            $" NumberOfBooksOrdered:{o.ListOfGoodsInTheOrder[0].NumberOfBooksOrdered} ISBN:{o.ListOfGoodsInTheOrder[0]?.Book?.ISBN}");
}*/

//Explicit loading 1
/*using (ApplicationContext db = new ApplicationContext(options))
{
    Book? book = db.Book.FirstOrDefault();
    if (book != null)
    {
        //db.ListOfGoodsInTheOrder.Where(u => u.ISBN == book.ISBN).Load();
        db.ListOfGoodsInTheOrder.Load();
        foreach (ListOfGoodsInTheOrder l in book.ListOfGoodsInTheOrder)
            Console.WriteLine($"OrderNumber:{l.OrderNumber} " +
                $"NumberOfBooksOrdered:{l.NumberOfBooksOrdered} ISBN:{book.ISBN}");
    }
}*/

//Explicit loading 2
/*using (ApplicationContext db = new ApplicationContext(options))
{
    Book? book = db.Book.FirstOrDefault();
    if (book != null)
    {
        db.Entry(book).Collection(l => l.ListOfGoodsInTheOrder).Load();
        foreach (var l in book.ListOfGoodsInTheOrder)
            Console.WriteLine($"OrderNumber:{l.OrderNumber} " +
                $"NumberOfBooksOrdered:{l.NumberOfBooksOrdered} ISBN:{book.ISBN}");
    }
}*/

//Lazy loading 1
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var orders = db.Order.ToList();
    //foreach (Order o in orders)
    //{
    //    Console.Write($"OrderNumber:{o.OrderNumber} CustomerPhone:{o.CustomerPhone} ");
    //    foreach (ListOfGoodsInTheOrder l in o!.ListOfGoodsInTheOrder)
    //    {
    //        Console.WriteLine();
    //        Console.Write($" NumberOfBooksOrdered:{l.NumberOfBooksOrdered} ");
    //        foreach (Book b in o.ListOfGoodsInTheOrder)
    //            Console.Write($"ISBN:{b.ISBN} ");
    //    }
    //    Console.WriteLine();
    //}
    foreach (Order o in orders)
        Console.WriteLine($"OrderNumber:{o.OrderNumber} CustomerPhone:{o.CustomerPhone}" +
            $" NumberOfBooksOrdered:{o.ListOfGoodsInTheOrder[0].NumberOfBooksOrdered} ISBN:{o.ListOfGoodsInTheOrder[0]?.Book?.ISBN}");
}*/

//Where
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Book.ToList();
    Console.WriteLine("Before");
    foreach (Book b in before)
        Console.WriteLine($"Title:{b.Title} YearOfPrinting:{b.YearOfPrinting} NumberOfPages:{b.NumberOfPages}");

    var result = db.Book.Where(p => p.YearOfPrinting > 2010 && p.NumberOfPages < 300);
    Console.WriteLine();
    Console.WriteLine("After");
    foreach (Book b in result)
        Console.WriteLine($"Title:{b.Title} YearOfPrinting:{b.YearOfPrinting} NumberOfPages:{b.NumberOfPages}");
}*/

//EF.Functions.Like
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Customer.ToList();
    Console.WriteLine("Before");
    foreach (Customer c in before)
        Console.WriteLine($"CustomerPhone:{c.CustomerPhone} CustomerFullName:{c.CustomerFullName} Email:{c.Email}");

    var result = db.Customer.Where(p => EF.Functions.Like(p.Email!, "%outlook.com"));
    Console.WriteLine();
    Console.WriteLine("After");
    foreach (Customer c in result)
        Console.WriteLine($"CustomerPhone:{c.CustomerPhone} CustomerFullName:{c.CustomerFullName} Email:{c.Email}");
}*/

//Find
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Payment.ToList();
    Console.WriteLine("Before");
    foreach (Payment p in before)
        Console.WriteLine($"PaymentNumber:{p.PaymentNumber} ActualAmount:{p.ActualAmount}");

    Payment? payment = db.Payment.Find(4);
    Console.WriteLine();
    Console.WriteLine("After");
    if (payment != null)
        Console.WriteLine($"PaymentNumber:{payment.PaymentNumber} ActualAmount:{payment.ActualAmount}");
}*/

//OrderBy
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Customer.ToList();
    Console.WriteLine("Before");
    foreach (Customer c in before)
        Console.WriteLine($"CustomerFullName:{c.CustomerFullName} DateOfBirth:{c.DateOfBirth}");

    var result = db.Customer.OrderByDescending(p => p.DateOfBirth);
    Console.WriteLine();
    Console.WriteLine("After");
    foreach (Customer c in result)
        Console.WriteLine($"CustomerFullName:{c.CustomerFullName} DateOfBirth:{c.DateOfBirth}");
}*/

//Join
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var result = from order in db.Order
                 join listofgoodsintheorder in db.ListOfGoodsInTheOrder on order.OrderNumber equals listofgoodsintheorder.OrderNumber
                 join book in db.Book on listofgoodsintheorder.ISBN equals book.ISBN
                 select new
                 {
                     order.OrderNumber,
                     order.Cost,
                     listofgoodsintheorder.NumberOfBooksOrdered,
                     book.Title
                 };
    foreach (var r in result)
        Console.WriteLine($"OrderNumber:{r.OrderNumber} Cost:{r.Cost} " +
            $"NumberOfBooksOrdered:{r.NumberOfBooksOrdered} Title:{r.Title}");
}*/

//GroupBy
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var result = from l in db.ListOfGoodsInTheOrder
                 group l by l.OrderNumber into g
                 select new
                 {
                     GroupedOrderNumber = g.Key,
                     Counter = g.Count()
                 };
    foreach (var group in result)
    {
        Console.WriteLine($"GroupedOrderNumber:{group.GroupedOrderNumber} Counter:{group.Counter}");
    }
}*/

//Union
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = from customer in db.Customer
                 join order in db.Order on customer.CustomerPhone equals order.CustomerPhone
                 select new
                 {
                     customer.CustomerFullName,
                     order.RecipientFullName,
                 };
    Console.WriteLine("Before");
    foreach (var b in before)
        Console.WriteLine($"CustomerFullName:{b.CustomerFullName} RecipientFullName:{b.RecipientFullName}");

    var result = db.Order.Select(p => new { UnionResult = p.RecipientFullName })
          .Union(db.Customer.Select(c => new { UnionResult = c.CustomerFullName }));
    Console.WriteLine();
    Console.WriteLine("After");
    foreach (var r in result)
        Console.WriteLine(r.UnionResult);
}*/

//Except
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = from customer in db.Customer
                 join order in db.Order on customer.CustomerPhone equals order.CustomerPhone
                 select new
                 {
                     customer.CustomerFullName,
                     order.RecipientFullName,
                 };
    Console.WriteLine("Before");
    foreach (var b in before)
        Console.WriteLine($"CustomerFullName:{b.CustomerFullName} RecipientFullName:{b.RecipientFullName}");

    var selector1 = db.Order.Select(p => new { ExceptResult = p.RecipientFullName });
    var selector2 = db.Customer.Select(p => new { ExceptResult = p.CustomerFullName });
    var result = selector1.Except(selector2);
    Console.WriteLine();
    Console.WriteLine("After");
    foreach (var r in result)
        Console.WriteLine(r.ExceptResult);
}*/

//Intersect
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = from customer in db.Customer
                 join order in db.Order on customer.CustomerPhone equals order.CustomerPhone
                 select new
                 {
                     customer.CustomerFullName,
                     order.RecipientFullName,
                 };
    Console.WriteLine("Before");
    foreach (var b in before)
        Console.WriteLine($"CustomerFullName:{b.CustomerFullName} RecipientFullName:{b.RecipientFullName}");

    var result = db.Order.Select(p => new { IntersectResult = p.RecipientFullName })
          .Intersect(db.Customer.Select(c => new { IntersectResult = c.CustomerFullName }));
Console.WriteLine();
    Console.WriteLine("After");
    foreach (var r in result)
        Console.WriteLine(r.IntersectResult);
}*/

//Any
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Customer.ToList();
    Console.WriteLine("Before");
    foreach (var b in before)
        Console.WriteLine($"CustomerFullName:{b.CustomerFullName}");

    bool result = db.Customer.Any(c => EF.Functions.Like(c.CustomerFullName!, "%Paul%"));
    Console.WriteLine();
    Console.WriteLine("After");
    Console.WriteLine($"Result:{result}");
}*/

//All
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var before = db.Order.ToList();
    Console.WriteLine("Before");
    foreach (var b in before)
        Console.WriteLine($"OrderNumber:{b.OrderNumber} OrderStatus:{b.OrderStatus}");

    bool result = db.Order.All(o => o.OrderStatus == true);
    Console.WriteLine();
    Console.WriteLine("After");
    Console.WriteLine($"Result:{result}");
}*/

//Count
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var data = from order in db.Order
                 join customer in db.Customer on order.CustomerPhone equals customer.CustomerPhone
                 select new
                 {
                     customer.CustomerPhone,
                     customer.CustomerFullName,
                 };
    Console.WriteLine("Before");
    foreach (var r in data)
        Console.WriteLine($"CustomerPhone:{r.CustomerPhone} CustomerFullName:{r.CustomerFullName} ");

    int result1 = data.Count();
    int result2= data.Count(c => EF.Functions.Like(c.CustomerFullName!, "%Patricia%"));
    Console.WriteLine();
    Console.WriteLine($"Result1:{result1}");
    Console.WriteLine($"Result2:{result2}");
}*/

//Sum Min Max Avearge
/*using (ApplicationContext db = new ApplicationContext(options))
{
    var data = db.Book.ToList();
    Console.WriteLine("Before");
    foreach (var b in data)
        Console.WriteLine($"ActualPrice:{b.ActualPrice} Title:{b.Title}");

    Console.WriteLine();
    double minPrice = db.Book.Min(b => b.ActualPrice);
    Console.WriteLine($"minPrice:{minPrice}");
    double maxPrice = db.Book.Max(b => b.ActualPrice);
    Console.WriteLine($"maxPrice:{maxPrice}");
    double avgPrice = db.Book.Average(b => b.ActualPrice);
    Console.WriteLine($"avgPrice:{avgPrice}");
    double myavgPrice = data.Sum(b => b.ActualPrice) / data.Count(b => b.ActualPrice != 0);
    Console.WriteLine($"myavgPrice:{myavgPrice}");
}*/

//знайти всіх клієнтів, які придбали книжок на сумму більше ніж S за період P і таким клієнтам зробити знижку Z%
/*using (ApplicationContext db = new ApplicationContext(options))
{
    double S = 20;
    DateTime periodStart = new DateTime(2022, 1, 1);
    DateTime periodEnd = new DateTime(2022, 4, 1);
    double Z = 10;
    var datain = from customer in db.Customer
                 join order in db.Order on customer.CustomerPhone equals order.CustomerPhone
                 join listofgoodsintheorder in db.ListOfGoodsInTheOrder on order.OrderNumber equals listofgoodsintheorder.OrderNumber
                 group order by order.OrderNumber into g
                 select new
                 {
                     orderNumber = g.Key,
                     orderDate = g.Select(s => s.OrderDate).FirstOrDefault(),
                     sum = g.Select(s => s.ListOfGoodsInTheOrder.Select(x => x.NumberOfBooksOrdered * x.OrderedPrice).Sum()).FirstOrDefault(),
                 };
    Console.WriteLine("Before");
    foreach (var d in datain)
        Console.WriteLine($"OrderNumber:{d.orderNumber} OrderDate:{d.orderDate:d} Sum:{d.sum}");

    var dataout = from d in datain
                  where (d.sum > S && (d.orderDate > periodStart && d.orderDate < periodEnd))
                  select new
                  {
                      d.orderNumber,
                      d.orderDate,
                      updatedSum = d.sum * (1 - Z / 100),
                  };
    Console.WriteLine();
    Console.WriteLine($"After filter Sum > {S}, period from {periodStart:d} to {periodEnd:d}, discount {Z}%");
    foreach (var d in dataout)
        Console.WriteLine($"OrderNumber:{d.orderNumber} OrderDate:{d.orderDate:d} Sum:{d.updatedSum}");

    var orders = db.Order.ToList();
    foreach (var d in dataout)
    {
            foreach (Order order in orders)
                if (order.OrderNumber == d.orderNumber)
                    order.Cost = d.updatedSum;
    }
    db.SaveChanges();
}*/
