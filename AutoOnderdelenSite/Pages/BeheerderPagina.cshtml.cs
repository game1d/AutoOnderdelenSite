using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BeheerderPaginaModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        public BeheerderPaginaModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }

        public IList<Particulier> Particulieren {  get; set; }
        [BindProperty]
        public IList<Particulier> OngebandeParticulieren { get; set; }= new List<Particulier>();
        [BindProperty]
        public IList<Particulier> GebandeParticulieren { get; set; } = new List<Particulier>();

        public IList<Bedrijf> Bedrijven { get; set; }
        [BindProperty]
        public IList<Bedrijf> OngebandeBedrijven { get; set; } = new List<Bedrijf>();
        [BindProperty]
        public IList<Bedrijf> GebandeBedrijven { get; set; } = new List<Bedrijf>();
        [BindProperty]
        public Beheerder Beheerder { get; set; }
        [BindProperty]
        public string UserRol { get; set; }

        public async void OnGet()
        {
            int id = Convert.ToInt32(Request.Cookies["UserId"]);
            Beheerder = await autoStoreDatabase.VindBeheerderOpUserId(id);
            UserRol = Request.Cookies["PartOfBedrijf"];

            Particulieren = await autoStoreDatabase.GetParticulierenAsync();

            Bedrijven = await autoStoreDatabase.GetBedrijvenAsync();

            foreach (var _part in Particulieren)
            {
                if (_part.BanStatus == "ban")
                {
                    GebandeParticulieren.Add(_part);
                }
                else { OngebandeParticulieren.Add(_part); }

            }
            foreach (var _bedrijf in Bedrijven)
            {
                if (_bedrijf.BanStatus == "ban")
                {
                    GebandeBedrijven.Add(_bedrijf);
                }
                else { OngebandeBedrijven.Add(_bedrijf); }

            }
        }

        public ActionResult OnPost()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("PartOfBedrijf");
            return RedirectToPage("MainPage");
        }

        public async Task<ActionResult> OnPostBanParticulier(int PartId)
        {
            Particulier ToBanPart = await autoStoreDatabase.VindParticulierOpUserId(PartId);
            ToBanPart.BanStatus = "ban";
            await autoStoreDatabase.UpdateParticulier(ToBanPart);
            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostUnBanParticulier(int PartId)
        {
            Particulier ToBanPart = await autoStoreDatabase.VindParticulierOpUserId(PartId);
            ToBanPart.BanStatus = null;
            await autoStoreDatabase.UpdateParticulier(ToBanPart);

            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostBanBedrijf(int PartId)
        {
            Bedrijf ToBanPart = await autoStoreDatabase.VindBedrijfOpUserId(PartId);
            ToBanPart.BanStatus = "ban";
            await autoStoreDatabase.UpdateBedrijf(ToBanPart);
            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostUnBanBedrijf(int PartId)
        {
            Bedrijf ToBanPart = await autoStoreDatabase.VindBedrijfOpUserId(PartId);
            ToBanPart.BanStatus = null;
            await autoStoreDatabase.UpdateBedrijf(ToBanPart);

            return RedirectToPage();
        }
    }
}
