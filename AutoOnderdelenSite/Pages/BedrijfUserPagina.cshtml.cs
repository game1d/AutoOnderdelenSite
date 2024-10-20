using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfUserPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public BedrijfUserPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public Bedrijf Bedrijf { get; set; }
        public async void OnGet()
        {
            int id = Convert.ToInt32( Request.Cookies["UserId"]);
            Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(id);

        }
        public ActionResult OnPost()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage("MainPage");
        }
    }
}
