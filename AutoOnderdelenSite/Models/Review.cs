using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int reviewerId { get; set; }
        public Particulier? ReviewerParticulier { get; set; }
        public string ReviewerName { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Score { get; set; }
        public string Beschrijving {  get; set; }
    }
}
