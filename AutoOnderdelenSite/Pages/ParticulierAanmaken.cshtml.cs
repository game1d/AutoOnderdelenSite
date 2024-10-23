using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class ParticulierAanmakenModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        public ParticulierAanmakenModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public Particulier Part { get; set; }
        [BindProperty]
        public string UserNameInput { get; set; }
        [BindProperty]
        public string WachtwoordInput { get; set; }
        [BindProperty]
        public string EmailInput { get; set; }
        [BindProperty]
        public string AdresInput { get; set; }
        [BindProperty]
        public string VoorNaamInput { get; set; }
        [BindProperty]
        public string AchterNaamInput { get; set; }
        [BindProperty]
        public string TelefoonNummerInput { get; set; }
        [BindProperty]
        public string BetaalGegevensInput { get; set; }

        [BindProperty]
        public string Errormessage {  get; set; }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            try
            {
                Part.UserName = UserNameInput;
                Part.Wachtwoord = HasherMaker.ToSHA256(WachtwoordInput);
                Part.Email = EmailInput;
                Part.Adres = AdresInput;
                Part.VoorNaam = VoorNaamInput;
                Part.AchterNaam = AchterNaamInput;
                Part.TelefoonNummer = TelefoonNummerInput;
                Part.BetaalGegevens = BetaalGegevensInput;

                await autoStoreDatabase.ToevoegenParticulier(Part);
                return RedirectToPage();
            }
            catch (Exception ex) { Errormessage = ex.Message; return Page(); }
        }
    }
}
