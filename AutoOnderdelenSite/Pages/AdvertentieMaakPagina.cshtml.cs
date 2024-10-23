using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class AdvertentieMaakPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public AdvertentieMaakPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }

        [BindProperty]
        public List<Product> products { get; set; }
        [BindProperty]
        public string advertentiesoort {  get; set; }
 
        [BindProperty]
        public int ProductIdInput { get; set; }
        [BindProperty]
        public int AantalInput { get; set; }
        [BindProperty]
        public string StaatProductInput { get; set; }
        [BindProperty]
        public double PrijsInput {  get; set; }


        public async void OnGet(string SoortAdvertentie)
        {
            products = await autoStoreDatabase.GetAlleProductenAsync();
            advertentiesoort = SoortAdvertentie;
        }
        
        public async Task<ActionResult> OnPostNieuwAdvertentieMaken()
        {
            NieuwProductAdvertentie NieuwProdAd = new NieuwProductAdvertentie();
            NieuwProdAd.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            NieuwProdAd.ProductId = ProductIdInput;
            NieuwProdAd.Aantal = AantalInput;
            NieuwProdAd.Prijs = PrijsInput;
            await autoStoreDatabase.VoegNieuwAdvertentieToe(NieuwProdAd);
            return RedirectToPage("BedrijfUserPagina");
        }
        public async Task<ActionResult> OnPostRefurbishedAdvertentieMaken()
        {
            RefurbishedAdvertentie refurbishedAdvertentie = new RefurbishedAdvertentie();
            refurbishedAdvertentie.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            refurbishedAdvertentie.ProductId = ProductIdInput;
            refurbishedAdvertentie.StaatProduct = StaatProductInput;
            refurbishedAdvertentie.Prijs = PrijsInput;
            await autoStoreDatabase.VoegRefurbishedAdvertentieToe(refurbishedAdvertentie);
            return RedirectToPage("BedrijfUserPagina");
        }
        public async Task<ActionResult> OnPostTweedeHandsAdvertentieMaken()
        {
            TweedeHandsAdvertentie TweedHands = new TweedeHandsAdvertentie();
            TweedHands.UserId=Convert.ToInt32(Request.Cookies["UserId"]);
            TweedHands.ProductId = ProductIdInput;
            TweedHands.StaatProduct= StaatProductInput;
            TweedHands.Prijs = PrijsInput;
            await autoStoreDatabase.VoegTweedeHandsAdvertentieToe(TweedHands);
            return RedirectToPage("ParticulierUserPagina");
        }
    }
}
