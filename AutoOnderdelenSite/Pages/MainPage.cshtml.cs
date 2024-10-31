using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

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

        public MainPageModel(AutoStoreDatabase _autoStoreDatabase/*, ILogger<MainPageModel> logger*/)
        {
            //_logger = logger;
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

        public IList<WikiArtikelParticulier> WikisParticulier { get; set; }

        [BindProperty]
        public IList<WikiArtikelParticulier> WikisParticulierResult { get; set; }

        public IList<WikiArtikelBedrijf> WikisBedrijf { get; set; }

        [BindProperty]
        public IList<WikiArtikelBedrijf> WikisBedrijfResult { get; set; }

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
            WikisParticulier = await autoStoreDatabase.GetArtikelParticuliersAsync();
            WikisBedrijf = await autoStoreDatabase.GetWikiArtikelBedrijfsAsync();

            foreach (var adv in tweedeHandsAdvertenties)
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

            foreach (var wiki in WikisParticulier)
            {
                if(wiki.Omschrijving.Contains(ZoekTerm))
                {
                    WikisParticulierResult.Add(wiki);
                }
            }
            foreach (var wiki in WikisBedrijf)
            {
                if (wiki.Omschrijving.Contains(ZoekTerm))
                {
                    WikisBedrijfResult.Add(wiki);
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
