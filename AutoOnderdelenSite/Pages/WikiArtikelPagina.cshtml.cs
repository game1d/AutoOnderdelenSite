using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class WikiArtikelPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;


        public WikiArtikelPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;

        }
        [BindProperty]
        public WikiArtikelParticulier WikiArtikelParticulier { get; set; }

        [BindProperty]
        public WikiArtikelBedrijf WikiArtikelBedrijf { get; set; }

        [BindProperty]
        public string WikiSoort {  get; set; }

        public async void OnGet(int WikiId, string wikiSoort)
        {
            if (wikiSoort == "Particulier") 
            {
                WikiArtikelParticulier = await autoStoreDatabase.GetSingletArtikelParticulierAsync(WikiId);
                WikiSoort = wikiSoort;
            }

            if (wikiSoort == "Bedrijf") 
            {
                WikiArtikelBedrijf = await autoStoreDatabase.GetSingletWikiArtikelBedrijfsAsync(WikiId);
                WikiSoort = wikiSoort;
            }


        }
    }
}
