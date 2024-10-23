namespace AutoOnderdelenSite.Models
{
    public class TweedeHandsAdvertentie : Advertenties
    {
        public string StaatProduct { get; set; }
        public Particulier User { get; set; }

        public List<Bieding>? biedingen { get; set; }
    }
}
