using System.ComponentModel.DataAnnotations;

namespace EFC
{
    public class Edition
    {
        [Key]
        public string EditionNumber { get; set; } = "";
        public string IssueNumber { get; set; } = "";
        public string PublishingHouse { get; set; } = "";
        public int Circulation { get; set; }

        public virtual Book? Book { get; set; }
    }
}