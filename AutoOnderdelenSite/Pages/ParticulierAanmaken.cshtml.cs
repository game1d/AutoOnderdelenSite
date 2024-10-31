using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class ParticulierAanmakenModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<ParticulierAanmakenModel> _logger;
        public ParticulierAanmakenModel(AutoStoreDatabase _autoStoreDatabase, ILogger<ParticulierAanmakenModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
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
            string _wachtwoord = WachtwoordInput;
            if (Validator.WachtwoordValidator(_wachtwoord)) {
                try
                {
                    Part.UserName = UserNameInput;
                    Part.Wachtwoord = HasherMaker.ToSHA256(WachtwoordInput);
                    Part.Email = UserNameInput;
                    Part.Adres = AdresInput;
                    Part.VoorNaam = VoorNaamInput;
                    Part.AchterNaam = AchterNaamInput;
                    Part.TelefoonNummer = TelefoonNummerInput;
                    Part.BetaalGegevens = BetaalGegevensInput;

                    await autoStoreDatabase.ToevoegenParticulier(Part);
                    _logger.LogInformation("Er is nieuw particulier toegevoegd. Het heeft de {Naam} en {Emailadres}.", Part.UserName, Part.Email);
                    return RedirectToPage();
                }
                catch (Exception ex) 
                { 
                    Errormessage = ex.Message;
                    _logger.LogError("Er is geprobeerd een nieuw bedrijf toe te voegen, maar het heeft gefaald. Het heeft de {Naam} en {Emailadres}.{error}", UserNameInput, UserNameInput, Errormessage);
                    return Page(); 
                }
            }
            else
            {
                Errormessage = "Je wachtwoord moet minstens 10 karakters lang zijn, een hoofdletter en een cijfer bevatten.";
                return Page();
            }
        }
    }
}
