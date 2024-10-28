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
        public IList<TweedeHandsAdvertentie> tweedeHandsAdvResult { get; set; }

        public IList<RefurbishedAdvertentie> refurbishedAdvertenties { get; set; }

        [BindProperty]
        public IList<RefurbishedAdvertentie> refurbishedAdvResult { get; set; }

        public IList<NieuwProductAdvertentie> nieuwAdvertenties { get; set; }

        [BindProperty]
        public IList<NieuwProductAdvertentie> nieuwAdvResult { get; set; }

        [BindProperty]
        public string currentUserId { get; set; }

        [BindProperty]
        public string partOfBedrijf {  get; set; }

        [BindProperty]
        public string ZoekTerm {  get; set; }

        public IList<Bedrijf> Bedrijven { get; set; }
        [BindProperty]
        public List<Bedrijf> BedrijfResultaat { get; set; }

        public IList<Particulier> Particulieren { get; set; }
        [BindProperty]
        public List<Particulier> PartResultaat { get; set; }
      


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
            currentUserId = Request.Cookies["UserId"];
            partOfBedrijf = Request.Cookies["PartOfBedrijf"];

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

        public async Task<ActionResult> OnPostZoekParticulier()
        {
            Particulieren = await autoStoreDatabase.GetParticulierenAsync();

            foreach (var _part in Particulieren)
            {
                if (_part.UserName.Contains(ZoekTerm))
                {
                    PartResultaat.Add(_part);
                }
            }
            return Page();
        }

        public async Task<ActionResult> OnPostZoekBedrijf()
        {
            Bedrijven = await autoStoreDatabase.GetBedrijvenAsync();

            foreach (var _bedrijf in Bedrijven)
            {
                if (_bedrijf.UserName.Contains(ZoekTerm))
                {
                    BedrijfResultaat.Add(_bedrijf);
                }
            }
            return Page();
        }
    }
}
