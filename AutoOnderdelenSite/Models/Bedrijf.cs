﻿namespace AutoOnderdelenSite.Models
{
    public class Bedrijf : User
    {
        public override int UserId { get; set; }
        public string Adres { get; set; }
        public string TelefoonNummer { get; set; }
        public string BetaalGegevens { get; set; }
        public List<RefurbishedAdvertentie>? RefurbishedAvertenties { get; set; }
        public List<NieuwProductAdvertentie>? NieuwProductAdvertenties { get; set; }
        public List<Review>? Reviews { get; set; }

    }
}