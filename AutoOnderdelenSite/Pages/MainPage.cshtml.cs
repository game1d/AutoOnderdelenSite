using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class MainPageModel : PageModel
    {
        //private readonly ILogger<MainPageModel> _logger;

        //public MainPageModel(ILogger<MainPageModel> logger)
        //{
        //    _logger = logger;
        //}
        private readonly AutoStoreDatabase autoStoreDatabase;

        public MainPageModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }

        public IList<TweedeHandsAdvertentie> tweedeHandsAdvertenties { get; set; }
        [BindProperty]
        public TweedeHandsAdvertentie tweedeHandsAdvertentie { get; set; }
        [BindProperty]
        public IList<TweedeHandsAdvertentie> tweedeHandsAdvResult { get; set; }

        public IList<RefurbishedAdvertentie> refurbishedAdvertenties { get; set; }
        [BindProperty]
        public RefurbishedAdvertentie refurbishedAdvertentie { get; set; }
        [BindProperty]
        public IList<RefurbishedAdvertentie> refurbishedAdvResult { get; set; }

        public IList<NieuwProductAdvertentie> nieuwAdvertenties { get; set; }
        [BindProperty]
        public NieuwProductAdvertentie nieuwAdvertentie { get; set; }
        [BindProperty]
        public IList<NieuwProductAdvertentie> nieuwAdvResult { get; set; }

        [BindProperty]
        public string currentUserId { get; set; }

        [BindProperty]
        public string partOfBedrijf {  get; set; }

        [BindProperty]
        public string ZoekTerm {  get; set; }

        public async void OnGet()
        {
            currentUserId = Request.Cookies["UserId"];
            partOfBedrijf = Request.Cookies["PartOfBedrijf"];

        }

        public ActionResult OnPost() 
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostZoek()
        {
            tweedeHandsAdvertenties = await autoStoreDatabase.GetTweedeHandsAdvertentiesAsync();
            refurbishedAdvertenties = await autoStoreDatabase.GetRefurbishedAdvertentiesAsync();
            nieuwAdvertenties = await autoStoreDatabase.GetNieuweProductAdvertentiesAsync();
            foreach(var adv in tweedeHandsAdvertenties)
            {
                if (adv.Product.ProductNaam.Contains(ZoekTerm))
                {
                    tweedeHandsAdvResult.Add(adv);
                }
            }
            foreach (var adv in refurbishedAdvertenties)
            {
                if (adv.Product.ProductNaam.Contains(ZoekTerm))
                {
                    refurbishedAdvResult.Add(adv);
                }
            }
            foreach (var adv in nieuwAdvertenties)
            {
                if (adv.Product.ProductNaam.Contains(ZoekTerm))
                {
                    nieuwAdvResult.Add(adv);
                }
            }

            return Page();
        }

    }
}
