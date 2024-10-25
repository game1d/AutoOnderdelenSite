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
        
        public WikiModel(AutoDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            WikiArtikelBedrijfs = await _context.WikiArtikelBedrijfsDb.Include( w => w.Product).ToListAsync();
            WikiArtikelParticuliers = await _context.WikiArtikelParticuliersDb.Include(w => w.Product).ToListAsync();
        }
           
       
    }
}
