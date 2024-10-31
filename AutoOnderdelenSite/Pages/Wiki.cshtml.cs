using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoOnderdelenSite.Pages
{
    public class WikiModel : PageModel
    {
        private readonly AutoDbContext _context;

        public List<WikiArtikelBedrijf> WikiArtikelBedrijfs {  get; set; }
        public List<WikiArtikelParticulier> WikiArtikelParticuliers { get; set; }
        private readonly AutoStoreDatabase autoStoreDatabase;
        public WikiModel(AutoDbContext context, AutoStoreDatabase _autoStoreDatabase)
        {
            _context = context;
            autoStoreDatabase = _autoStoreDatabase;
        }
        public async Task OnGetAsync()
        {
            WikiArtikelBedrijfs = await autoStoreDatabase.GetWikiArtikelBedrijfsAsync();
            WikiArtikelParticuliers = await autoStoreDatabase.GetArtikelParticuliersAsync();
        }
           
       
    }
}
