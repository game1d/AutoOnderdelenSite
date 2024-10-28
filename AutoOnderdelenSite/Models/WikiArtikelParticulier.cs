using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class WikiArtikelParticulier
    {
        [Key]
        public int WikiId {  get; set; }
        public string? AuteurParticulier { get; set; }
        public Product Product { get; set; }
        public string Omschrijving {  get; set; }
        public string Foto { get; set; }
    }
}
