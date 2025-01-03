using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoOnderdelenSite.Pages
{
    public class AdvertentieMaakPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<AdvertentieMaakPaginaModel> _logger;

        public AdvertentieMaakPaginaModel(AutoStoreDatabase _autoStoreDatabase, ILogger<AdvertentieMaakPaginaModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
        }

        [BindProperty]
        public List<Product> products { get; set; }
        [BindProperty]
        public string advertentiesoort { get; set; }

        [BindProperty]
        public int ProductIdInput { get; set; }
        [BindProperty]
        public int AantalInput { get; set; }
        [BindProperty]
        public string StaatProductInput { get; set; }
        [BindProperty]
        public string PrijsInput { get; set; }
        [BindProperty]
        public string message { get; set; }


        public async void OnGet(string SoortAdvertentie)
        {
            products = await autoStoreDatabase.GetAlleProductenAsync();
            advertentiesoort = SoortAdvertentie;
        }

        public async Task<ActionResult> OnPostNieuwAdvertentieMaken()
        {
            message = Validator.BedragValidator(PrijsInput);
            if (message == "")
            {
                NieuwProductAdvertentie NieuwProdAd = new NieuwProductAdvertentie();
                NieuwProdAd.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
                NieuwProdAd.ProductId = ProductIdInput;
                NieuwProdAd.Aantal = AantalInput;
                NieuwProdAd.Prijs = Convert.ToDouble(PrijsInput);
                await autoStoreDatabase.VoegNieuwAdvertentieToe(NieuwProdAd);
                Bedrijf _bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
                _logger.LogInformation("{Bedrijf} heeft een advertentie geplaatst voor een nieuw product.", _bedrijf.UserName);
                return RedirectToPage("BedrijfUserPagina");
            }
            else
            {
                products = await autoStoreDatabase.GetAlleProductenAsync();
                advertentiesoort = "nieuw";

                return Page();
            }
        }
        public async Task<ActionResult> OnPostRefurbishedAdvertentieMaken()
        {
            message = Validator.BedragValidator(PrijsInput);
            if (message == "")
            {
                RefurbishedAdvertentie refurbishedAdvertentie = new RefurbishedAdvertentie();
                refurbishedAdvertentie.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
                refurbishedAdvertentie.ProductId = ProductIdInput;
                refurbishedAdvertentie.StaatProduct = StaatProductInput;
                refurbishedAdvertentie.Prijs = Convert.ToDouble(PrijsInput);
                await autoStoreDatabase.VoegRefurbishedAdvertentieToe(refurbishedAdvertentie);
                Bedrijf _bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
                _logger.LogInformation("{Bedrijf} heeft een advertentie geplaatst voor een refurbished product.", _bedrijf.UserName);
                return RedirectToPage("BedrijfUserPagina");
            }
            else
            {
                products = await autoStoreDatabase.GetAlleProductenAsync();
                advertentiesoort = "refurbished";

                return Page();
            }
        }
        public async Task<ActionResult> OnPostTweedeHandsAdvertentieMaken()
        {
            message = Validator.BedragValidator(PrijsInput);
            if (message == "")
            {
                TweedeHandsAdvertentie TweedHands = new TweedeHandsAdvertentie();
                TweedHands.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
                TweedHands.ProductId = ProductIdInput;
                TweedHands.StaatProduct = StaatProductInput;
                TweedHands.Prijs = Convert.ToDouble(PrijsInput);
                await autoStoreDatabase.VoegTweedeHandsAdvertentieToe(TweedHands);
                Particulier _particulier = await autoStoreDatabase.VindParticulierOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
                _logger.LogInformation("{Particulier} heeft een advertentie geplaatst voor een 2ehands product.", _particulier.UserName);
                return RedirectToPage("ParticulierUserPagina");
            }
            else
            {
                products = await autoStoreDatabase.GetAlleProductenAsync();
                advertentiesoort = "tweedehands";

                return Page();
            }
        }
    }
}
