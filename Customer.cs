using System.ComponentModel.DataAnnotations;

namespace Multithreading
{
    public class Customer
    {
        [Key]
        public string? CustomerPhone { get; set; }
        public string? CustomerFullName { get; set; }

        public virtual List<OrderedBook> OrderedBook { get; set; } = new List<OrderedBook>();
    }
}