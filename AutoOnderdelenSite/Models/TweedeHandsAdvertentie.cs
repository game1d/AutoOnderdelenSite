namespace AutoOnderdelenSite.Models
{
    public class TweedeHandsAdvertentie : Advertenties
    {
        public string StaatProduct { get; set; }
        public Particulier User { get; set; }

    }
}
