using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class Order
    {
        [Key]
        public int OrderNumber { get; set; }
        public double Cost { get; set; }
        public string CustomerPhone { get; set; } = "";
        public string RecipientsPhone { get; set; } = "";
        public string RecipientFullName { get; set; } = "";
        public int PaymentNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string DeliveryMethod { get; set; } = "";
        public string DeliveryService { get; set; } = "";
        public string DeliveryAddress { get; set; } = "";

        public virtual Customer? Customer { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual List<ListOfGoodsInTheOrder> ListOfGoodsInTheOrder { get; set; } = new List<ListOfGoodsInTheOrder>();
    }
}