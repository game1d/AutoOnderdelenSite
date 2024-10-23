namespace AutoOnderdelenSite.Models
{
    public class NieuwProductAdvertentie : Advertenties
    {
        public List<Review> Reviews { get; set; }
        public Bedrijf User { get; set; }
        public int Aantal { get; set; }

        public List<NieuwKoop>? Koop { get; set; }
    }
}
