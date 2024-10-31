using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class WikiArtikelBedrijf
    {
        [Key]
        public int WikiId { get; set; }
        public int UserId {  get; set; }
        public Bedrijf User {get; set;}
        public int ProductId {  get; set; }
        public Product Product {  get; set; }
        public string Omschrijving {  get; set; }
        //public string Foto {  get; set; }//Niet genoeg tijd.

    }
}
