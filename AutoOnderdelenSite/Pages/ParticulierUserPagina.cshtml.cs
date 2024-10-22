using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class ParticulierUserPaginaModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public ParticulierUserPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public Particulier Particulier { get; set; }
        [BindProperty]
        public int AdId {  get; set; }
        public async void OnGet()
        {
            int id = Convert.ToInt32(Request.Cookies["UserId"]);
            Particulier = await autoStoreDatabase.VindParticulierOpUserId(id);
        }
        public ActionResult OnPost()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage("MainPage");
        }

        public async Task<ActionResult> OnPostDeleteAd(int Adid)
        {
            autoStoreDatabase.VerwijderTweedeHandsAdvertentie(Adid);

            return RedirectToPage();
        }
    }
}
