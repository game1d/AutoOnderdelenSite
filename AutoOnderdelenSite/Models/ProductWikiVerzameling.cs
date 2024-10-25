using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class ProductWikiVerzameling
    {
        [Key]
        public int VerzamelingId{  get; set; }
        public Product Product { get; set; }
        public List<WikiArtikelBedrijf> WikiBedrijf { get; set; } = new List<WikiArtikelBedrijf>();
        public List<WikiArtikelParticulier> WikiParticulier { get; set; } = new List<WikiArtikelParticulier>();
    }
}
