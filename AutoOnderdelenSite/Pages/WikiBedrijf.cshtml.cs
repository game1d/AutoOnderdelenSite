using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace AutoOnderdelenSite.Pages
{
    public class WikiBedrijfModel : PageModel
    {
        private readonly AutoDbContext context;

        [BindProperty]
        public WikiArtikelBedrijf WikiArtikel { get; set; }

        public WikiBedrijfModel(AutoDbContext context)
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
            context.WikiArtikelBedrijfsDb.Add(WikiArtikel);
            await context.SaveChangesAsync();
            return RedirectToPage("WikiBedrijf");
        }
    }
}
