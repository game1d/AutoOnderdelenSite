namespace AutoOnderdelenSite.Models
{
    public class ProductWikiVerzameling
    {
        public int Verzameling {  get; set; }
        public Product Product { get; set; }
        public List<WikiArtikelBedrijf> WikiBedrijf { get; set; } = new List<WikiArtikelBedrijf>();
        public List<WikiArtikelParticulier> WikiParticulier { get; set; } = new List<WikiArtikelParticulier>();
    }
}
