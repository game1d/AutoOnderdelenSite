﻿namespace AutoOnderdelenSite.Models
{
    public class RefurbishedAdvertentie : Advertenties
    {
        public string StaatProduct { get; set; }
        public Bedrijf User { get; set; }

        public List<KoopRefurbished>? KoopRefurbishedList { get; set; }
    }
}
