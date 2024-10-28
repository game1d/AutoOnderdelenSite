using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int reviewerId { get; set; }
        public Particulier? ReviewerParticulier { get; set; }//Als de reviewer verdwijnt hoeft de review niet weggegooid te worden.
        public string ReviewerName { get; set; }//Dit maakt het dat men nog altijd kan nakijken wie d review heeft gegeven.


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Score { get; set; }
        public string Beschrijving {  get; set; }
    }
}
