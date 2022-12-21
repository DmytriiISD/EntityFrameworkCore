using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class Book
    {
        [Key]
        public string ISBN { get; set; } = "";
        public string EditionNumber { get; set; } = "";
        public string? Title { get; set; }
        public string? AuthorsName { get; set; }
        public string? Genre { get; set; }
        public int? NumberOfPages { get; set; }
        public int? YearOfPrinting { get; set; }
        public int NumberOfBooksInStock { get; set; }
        public double ActualPrice { get; set; }

        public virtual List<Edition> Edition { get; set; } = new List<Edition>();
        public virtual List<ListOfGoodsInTheOrder> ListOfGoodsInTheOrder { get; set; } = new List<ListOfGoodsInTheOrder>();
    }
}