namespace AutoOnderdelenSite.Models
{
    public class ParticulierReview:Review
    {
        public int GereviewdeId {  get; set; }

        public Particulier GereviewdeParticulier { get; set; }

    }
}
