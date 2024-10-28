using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoOnderdelenSite.Pages
{
    public class WikiParticulierModel : PageModel
    {
        private readonly AutoDbContext context;

        [BindProperty]
        public WikiArtikelParticulier WikiArtikel { get; set; }

        public WikiParticulierModel(AutoDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            context.WikiArtikelParticuliersDb.Add(WikiArtikel);
            await context.SaveChangesAsync();
            return RedirectToPage("WikiParticulier");
        }

        
    }
}
