using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfUserPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<BedrijfUserPaginaModel> _logger;

        public BedrijfUserPaginaModel(AutoStoreDatabase _autoStoreDatabase, ILogger<BedrijfUserPaginaModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
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
            Product nieuwProduct= new Product();
            nieuwProduct.ProductType= ProductTypeInput;
            nieuwProduct.ProductNaam= ProductNaamInput;
            nieuwProduct.Omschrijving= OmSchrijvingInput;
            await autoStoreDatabase.VoegProductToe(nieuwProduct);
            Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));//Eigenlijk ook niet de mooiste oplossing.
            _logger.LogInformation("{Bedrijf} heeft een nieuw product type toegevoegd.", Bedrijf.UserName);
            return RedirectToPage();
        }
        public async Task<ActionResult> OnPostDeleteRefAd(int Adid)
        {
            autoStoreDatabase.VerwijderRefurbishedAdvertentie(Adid);
            Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));//Eigenlijk ook niet de mooiste oplossing.
            _logger.LogInformation("{Bedrijf} heeft een refurbished advertentie gedeleted.", Bedrijf.UserName);

            return RedirectToPage();
        }
        public async Task<ActionResult> OnPostDeletenieuwAd(int Adid)
        {
            autoStoreDatabase.VerwijderNieuwAdvertentie(Adid);
            Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));//Eigenlijk ook niet de mooiste oplossing.
            _logger.LogInformation("{Bedrijf} heeft een nieuw product advertentie gedeleted.", Bedrijf.UserName);
            return RedirectToPage();
        }
    }
}
