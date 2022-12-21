using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class Payment
    {
        [Key]
        public int PaymentNumber { get; set; }
        public double ActualAmount { get; set; }

        public virtual List<Order> Order { get; set; } = new List<Order>();
    }
}