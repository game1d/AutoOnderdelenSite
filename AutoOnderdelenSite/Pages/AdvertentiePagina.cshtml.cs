using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class AdvertentiePaginaModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        public AdvertentiePaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }

        [BindProperty]
        public string AdvertentieSoort { get; set; }
        [BindProperty]
        public TweedeHandsAdvertentie TweedAdv { get; set; }
        [BindProperty]
        public RefurbishedAdvertentie RefurAdv { get; set; }
        [BindProperty]
        public NieuwProductAdvertentie NieuwAdv { get; set; }
        [BindProperty]
        public string KoperNaamInput { get; set; }
        [BindProperty]
        public string KoperAdresInput { get; set; }
        [BindProperty]
        public string BetaalGegevens { get; set; }
        [BindProperty]
        public double Bedrag { get; set; }
        [BindProperty]
        public int AdvertentieId { get; set; }
        [BindProperty]
        public int Aantal { get; set; }

        public async void OnGet(string advertentieSoort, int advertentieId)
        {
            if (advertentieSoort == "Tweedehands")
            {
                AdvertentieSoort = advertentieSoort;
                TweedAdv = await autoStoreDatabase.GetTweedeHandsAdvertentie(advertentieId);
            }

            else if (advertentieSoort == "Refurbished")
            {
                AdvertentieSoort = advertentieSoort;
                RefurAdv = await autoStoreDatabase.GetRefurbishedAdvertentie(advertentieId);
            }

            else if (advertentieSoort == "Nieuw")
            {
                AdvertentieSoort = advertentieSoort;
                NieuwAdv = await autoStoreDatabase.GetNieuwAdvertentie(advertentieId);
            }

        }

        public async Task<ActionResult> OnPostBied()
        {
            Bieding _bieding = new Bieding();

            _bieding.AdvertentieId = AdvertentieId;
            _bieding.KoperNaam = KoperNaamInput;
            _bieding.KoperAdres = KoperAdresInput;
            _bieding.Betaalgegevens = BetaalGegevens;
            _bieding.Bedrag = Bedrag;
            await autoStoreDatabase.MaakBieding(_bieding);
            return RedirectToPage("/MainPage");
        }

        public async Task<ActionResult> OnPostKoopRef()
        {
            KoopRefurbished _RefurKoop = new KoopRefurbished();

            _RefurKoop.AdvertentieId = AdvertentieId;
            _RefurKoop.KoperNaam = KoperNaamInput;
            _RefurKoop.KoperAdres = KoperAdresInput;
            _RefurKoop.Betaalgegevens = BetaalGegevens;
            _RefurKoop.Bedrag = Bedrag;
            await autoStoreDatabase.MaakRefurKoop(_RefurKoop);
            return RedirectToPage("/MainPage");
        }

        public async Task<ActionResult> OnPostKoopNieuw()
        {
            NieuwKoop _nieuwKoop = new NieuwKoop();

            _nieuwKoop.AdvertentieId = AdvertentieId;
            _nieuwKoop.KoperNaam = KoperNaamInput;
            _nieuwKoop.KoperAdres = KoperAdresInput;
            _nieuwKoop.Betaalgegevens = BetaalGegevens;
            _nieuwKoop.Aantal = Aantal;
            _nieuwKoop.TotaalBedrag = (Bedrag * Aantal);
            await autoStoreDatabase.MaakNieuwKoop(_nieuwKoop);
            return RedirectToPage("/MainPage");
        }
    }
}
