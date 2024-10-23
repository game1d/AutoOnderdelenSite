
using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class Advertenties
    {
        [Key]
        public int AdvertentieId { get; set; }
        public int UserId { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public double Prijs { get; set; }

    }
}
