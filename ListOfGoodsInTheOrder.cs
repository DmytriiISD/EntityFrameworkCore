using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class ListOfGoodsInTheOrder
    {
        [Key]
        public int ProductNumberInTheOrder { get; set; }
        public int OrderNumber { get; set; }
        public string ISBN { get; set; } = "";
        public int NumberOfBooksOrdered { get; set; }
        public double OrderedPrice { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Book? Book { get; set; }
    }
}