using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfUserPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public BedrijfUserPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public Bedrijf Bedrijf { get; set; }
        [BindProperty]
        public string ProductNaamInput {  get; set; }
        [BindProperty]
        public string OmSchrijvingInput { get; set; }
        [BindProperty]
        public string ProductTypeInput { get; set; }

        public async void OnGet()
        {
            int id = Convert.ToInt32( Request.Cookies["UserId"]);
            Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(id);

        }
        public ActionResult OnPost()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage("MainPage");
        }

        public async Task<ActionResult> OnPostVoegProductToe()
        {
            Product niewProduct= new Product();
            niewProduct.ProductType= ProductTypeInput;
            niewProduct.ProductNaam= ProductNaamInput;
            niewProduct.Omschrijving= OmSchrijvingInput;
            await autoStoreDatabase.VoegProductToe(niewProduct);

            return RedirectToPage();
        }
        public async Task<ActionResult> OnPostDeleteRefAd(int Adid)
        {
            autoStoreDatabase.VerwijderRefurbishedAdvertentie(Adid);

            return RedirectToPage();
        }
        public async Task<ActionResult> OnPostDeletenieuwAd(int Adid)
        {
            autoStoreDatabase.VerwijderNieuwAdvertentie(Adid);

            return RedirectToPage();
        }
    }
}
