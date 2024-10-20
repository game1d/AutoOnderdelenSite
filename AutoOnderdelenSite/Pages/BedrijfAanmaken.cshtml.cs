using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfAanmakenModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public BedrijfAanmakenModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public Bedrijf Bedrijf { get; set; }
        [BindProperty]
        public string UserNameInput { get; set; }
        [BindProperty]
        public string WachtwoordInput { get; set; }
        [BindProperty]
        public string EmailInput { get; set; }
        [BindProperty]
        public string AdresInput { get; set; }
 
        [BindProperty]
        public string TelefoonNummerInput { get; set; }
        [BindProperty]
        public string BetaalGegevensInput { get; set; }

        [BindProperty]
        public string Errormessage { get; set; }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            try
            {
                Bedrijf.UserName = UserNameInput;
                Bedrijf.Wachtwoord = WachtwoordInput;
                Bedrijf.Email = EmailInput;
                Bedrijf.Adres = AdresInput;
                Bedrijf.TelefoonNummer = TelefoonNummerInput;
                Bedrijf.BetaalGegevens = BetaalGegevensInput;

                await autoStoreDatabase.ToevoegenBedrijf(Bedrijf);
                return RedirectToPage();
            }
            catch (Exception ex) { Errormessage = ex.Message; return Page(); }
        }
    }
}
