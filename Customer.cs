using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class Customer
    {
        [Key]
        public string CustomerPhone { get; set; } = "";
        public string CustomerFullName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }

        public virtual List<Order> Order { get; set; } = new List<Order>();
    }
}