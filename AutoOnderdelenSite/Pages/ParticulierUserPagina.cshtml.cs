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
        public int AdId { get; set; }

        [BindProperty]
        public List<TweedeHandsAdvertentie> lijstAdv { get; set; }

        public IList<Particulier> Particulieren { get; set; }
        [BindProperty]
        public List<Particulier> PartResultaat { get; set; }

        public IList<Bedrijf> Bedrijven { get; set; }
        [BindProperty]
        public List<Bedrijf> BedrijfResultaat { get; set; }

        [BindProperty]
        public string ZoekTerm { get; set; }

        public async void OnGet()
        {
            int id = Convert.ToInt32(Request.Cookies["UserId"]);
            Particulier = await autoStoreDatabase.VindParticulierOpUserId(id);
            lijstAdv = await autoStoreDatabase.VindTweedeHandsBiedingenOpEigenaar(id);
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

        public async Task<ActionResult> OnPostZoekParticulier()
        {
            int id = Convert.ToInt32(Request.Cookies["UserId"]);
            Particulier = await autoStoreDatabase.VindParticulierOpUserId(id);
            lijstAdv = await autoStoreDatabase.VindTweedeHandsBiedingenOpEigenaar(id);

            Particulieren = await autoStoreDatabase.GetParticulierenAsync();

            foreach (var _part in Particulieren)
            {
                if (_part.UserName.Contains(ZoekTerm) && _part.UserId != id)
                {
                    PartResultaat.Add(_part);
                }
            }
            return Page();
        }

        public async Task<ActionResult> OnPostZoekBedrijf()
        {
            int id = Convert.ToInt32(Request.Cookies["UserId"]);
            Particulier = await autoStoreDatabase.VindParticulierOpUserId(id);
            lijstAdv = await autoStoreDatabase.VindTweedeHandsBiedingenOpEigenaar(id);

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
