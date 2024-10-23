namespace AutoOnderdelenSite.Models
{
    public class Particulier : User
    {
        
        public string Adres { get; set; }
        public string VoorNaam {  get; set; }
        public string AchterNaam {  get; set; }
        public string? TelefoonNummer { get; set; }
        public string? BetaalGegevens { get; set; }
        public List<TweedeHandsAdvertentie>? TweedeHandsAdvertenties { get; set; }
        public List<Review>? Reviews { get; set; }

    }
}
