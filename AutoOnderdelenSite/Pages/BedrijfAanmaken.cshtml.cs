using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfAanmakenModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<BedrijfAanmakenModel> _logger;

        public BedrijfAanmakenModel(AutoStoreDatabase _autoStoreDatabase, ILogger<BedrijfAanmakenModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
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
            if (UserNameInput != null && UserNameInput!=null && EmailInput!=null && AdresInput!=null && TelefoonNummerInput!= null && BetaalGegevensInput !=null)
            {

                string _wachtwoord = WachtwoordInput;
                if (Validator.WachtwoordValidator(_wachtwoord))
                {

                    try
                    {
                        Bedrijf.UserName = UserNameInput;
                        Bedrijf.Wachtwoord = HasherMaker.ToSHA256(UserNameInput);
                        Bedrijf.Email = EmailInput;
                        Bedrijf.Adres = AdresInput;
                        Bedrijf.TelefoonNummer = TelefoonNummerInput;
                        Bedrijf.BetaalGegevens = BetaalGegevensInput;

                        await autoStoreDatabase.ToevoegenBedrijf(Bedrijf);
                        _logger.LogInformation("Er is nieuw bedrijf toegevoegd. Het heeft de {Naam} en {Emailadres}.", Bedrijf.UserName, Bedrijf.Email);
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
            else {
                Errormessage = "Vul alle benodigde gegevens in.";
                return Page();
            }
        }
    }
}