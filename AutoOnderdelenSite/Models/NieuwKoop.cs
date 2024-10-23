using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    public class NieuwKoop
    {
        [Key]
        public int KoopId { get; set; }

        public int AdvertentieId {  get; set; }
        public NieuwProductAdvertentie Advertentie { get; set; }
        
        public string KoperNaam {  get; set; }
        public string KoperAdres {  get; set; }
        public string Betaalgegevens {  get; set; }
        public int Aantal {  get; set; }
        public double TotaalBedrag {  get; set; }

    }
}
