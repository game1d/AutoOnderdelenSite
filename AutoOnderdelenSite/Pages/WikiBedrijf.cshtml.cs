using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using SQLitePCL;

namespace AutoOnderdelenSite.Pages
{
    public class WikiBedrijfModel : PageModel
    {
        private readonly AutoDbContext context;

        [BindProperty]
        public WikiArtikelBedrijf WikiArtikel { get; set; }

       
        private readonly AutoStoreDatabase autoStoreDatabase;

        [BindProperty]
        public string WikiOmschrijving { get; set; }
        [BindProperty]
        public int ProductIdInput {  get; set; }

        [BindProperty]
        public List<Product> products { get; set; }

        public WikiBedrijfModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        public async void OnGet()
        {
            products = await autoStoreDatabase.GetAlleProductenAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            WikiArtikel.ProductId=ProductIdInput;
            WikiArtikel.Omschrijving = WikiOmschrijving;
            WikiArtikel.UserId= Convert.ToInt32(Request.Cookies["UserId"]); 
            
            
            await autoStoreDatabase.ToevoegenWikiArtikelBedrijf(WikiArtikel);

            return RedirectToPage("WikiBedrijf");
        }
    }
}
