namespace AutoOnderdelenSite.Models
{
    public class BedrijfReview : Review
    {
        public int GereviewdeId { get; set; }

        public Bedrijf GereviewdBedrijf { get; set; }

    }
}
