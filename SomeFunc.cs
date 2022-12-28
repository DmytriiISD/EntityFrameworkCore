using Microsoft.EntityFrameworkCore;

namespace Multithreading
{
    public class SomeFunc
    {
        public uint counter = 0;
        public object locker = new();
        private DbContextOptions<ApplicationContext> options;
        private SampleContextFactory dbfactory = new SampleContextFactory();

        public SomeFunc(DbContextOptions<ApplicationContext> options)
        {
            this.options = options;
        }

        public void AddTasks(int quantity)
        {
            for (uint i = 0; i < quantity; i++)
            {
                Task myTask = new Task(AddBook);
                myTask.Start();
                myTask.Wait();
            }
        }

        public void AddMassTasks(int quantity)
        {
            Task[] tasks = new Task[quantity];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(AddBook);
            }
            Task.WaitAll(tasks);
        }

        public void AddThreads(int quantity)
        {
            for (uint i = 0; i < quantity; i++)
            {
                Thread myThread = new Thread(AddBook);
                myThread.Start();
            }
        }

        public void AddBook()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                lock (locker)
                {
                    counter++;
                    db.OrderedBook.AddAsync(new OrderedBook { ISBN = $"ISBN#{counter}", Title = "SomeTitle" });
                    db.SaveChanges();
                }
            }
        }
    }
}