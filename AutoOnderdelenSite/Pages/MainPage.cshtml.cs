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

        public async void OnGet()
        {
            particulieren = await autoStoreDatabase.GetParticulierenAsnyc();
            tweedeHandsAdvertenties = await autoStoreDatabase.GetTweedeHandsAdvertentiesAsync();
        }


    }
}
