﻿namespace AutoOnderdelenSite.Models
{
    public class WikiArtikelParticulier
    {
        public int WikiId {  get; set; }
        public Particulier AuteurParticulier { get; set; }
        public Product Product { get; set; }
        public String Omschrijving {  get; set; }
        public string Foto { get; set; }
    }
}
