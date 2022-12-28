using System.ComponentModel.DataAnnotations;

namespace Multithreading
{
    public class OrderedBook
    {
        [Key]
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? CustomerPhone { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}