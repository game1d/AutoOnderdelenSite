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
        public string AdvertentieSoort {  get; set; }
        [BindProperty]
        public TweedeHandsAdvertentie TweedAdv { get; set; }
        [BindProperty]
        public RefurbishedAdvertentie RefurAdv { get; set; }
        [BindProperty]
        public NieuwProductAdvertentie NieuwAdv { get; set; }

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
                RefurAdv =await autoStoreDatabase.GetRefurbishedAdvertentie(advertentieId);
            }

            else if (advertentieSoort == "Nieuw")
            {
                AdvertentieSoort = advertentieSoort;
                NieuwAdv = await autoStoreDatabase.GetNieuwAdvertentie(advertentieId);
            }

        }
    }
}
