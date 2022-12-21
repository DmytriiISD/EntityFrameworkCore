using Microsoft.EntityFrameworkCore;

namespace EFC
{
    public class BookManagment
    {
        private DbContextOptions<ApplicationContext> options;
        private SampleContextFactory dbfactory = new SampleContextFactory();

        public BookManagment(DbContextOptions<ApplicationContext> options)
        {
            this.options = options;
        }

        public void AddBook(Book somebook)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Book.Add(somebook);
                db.SaveChanges();
            }
        }

        public void EditBookPrice(string isbn)
        {
            using (ApplicationContext db = dbfactory.CreateDbContext(new string[] { }))
            {
                Book? editbook = db.Book.FirstOrDefault(x => x.ISBN == isbn);
                if (editbook != null)
                {
                    editbook.ActualPrice = 1.1 * editbook.ActualPrice;
                    db.SaveChanges();
                }
            }
        }
    }
}