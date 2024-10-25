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
            string _wachtwoord = WachtwoordInput;
            if (Validator.WachtwoordValidator(_wachtwoord))
            {
                try
                {
                    Bedrijf.UserName = UserNameInput;
                    Bedrijf.Wachtwoord = HasherMaker.ToSHA256(WachtwoordInput);
                    Bedrijf.Email = EmailInput;
                    Bedrijf.Adres = AdresInput;
                    Bedrijf.TelefoonNummer = TelefoonNummerInput;
                    Bedrijf.BetaalGegevens = BetaalGegevensInput;

                    await autoStoreDatabase.ToevoegenBedrijf(Bedrijf);
                    return RedirectToPage();
                }
                catch (Exception ex) { Errormessage = ex.Message; return Page(); }
            }
            else 
            { 
                Errormessage = "Je wachtwoord moet minstens 10 karakters lang zijn, een hoofdletter en een cijfer bevatten."; 
                return Page(); 
            }
        }
    }
}
