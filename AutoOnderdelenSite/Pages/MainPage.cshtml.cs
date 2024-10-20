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
        public IList<Particulier> particulieren { get; set; }
        [BindProperty]
        public Particulier particulier { get; set; }
        public IList<TweedeHandsAdvertentie> tweedeHandsAdvertenties { get; set; }
        [BindProperty]
        public TweedeHandsAdvertentie tweedeHandsAdvertentie { get; set; }
        [BindProperty]
        public string currentUserId { get; set; }

        [BindProperty]
        public string partOfBedrijf {  get; set; }
        public async void OnGet()
        {
            currentUserId = Request.Cookies["UserId"];
            partOfBedrijf = Request.Cookies["PartOfBedrijf"];

            particulieren = await autoStoreDatabase.GetParticulierenAsnyc();
            tweedeHandsAdvertenties = await autoStoreDatabase.GetTweedeHandsAdvertentiesAsync();
        }

        public ActionResult OnPost() 
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage();
        }

    }
}
