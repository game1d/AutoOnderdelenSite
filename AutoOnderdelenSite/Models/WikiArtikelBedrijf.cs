using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class WikiArtikelBedrijf
    {
        [Key]
        public int WikiId { get; set; }
        public Bedrijf AuteurBedrijf {get; set;}
        public Product Product {  get; set; }
        public string Omschrijving {  get; set; }
        public string Foto {  get; set; }

    }
}
