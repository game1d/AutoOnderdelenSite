using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class Bieding
    {
        [Key]
        public int KoopId { get; set; }

        public int AdvertentieId { get; set; }
        public TweedeHandsAdvertentie Advertentie { get; set; }

        public string KoperNaam { get; set; }
        public string KoperAdres { get; set; }
        public string Betaalgegevens { get; set; }
        public double Bedrag { get; set; }
    }
}
