using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class WijzigUserDataPaginaModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<ReviewschrijfPaginaModel> _logger;



        public WijzigUserDataPaginaModel(AutoStoreDatabase _autoStoreDatabase, ILogger<ReviewschrijfPaginaModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
        }

        [BindProperty]
        public string currentUserId { get; set; }

        [BindProperty]
        public string partOfBedrijf { get; set; }

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

        [BindProperty]
        public string VoorNaamInput { get; set; }
        [BindProperty]
        public string AchterNaamInput { get; set; }
        [BindProperty]
        public Particulier Part { get; set; }
        [BindProperty]
        public string NieuwWachtwoord { get; set; }

        [BindProperty]
        public string EmailForCheck { get; set; }
        [BindProperty]
        public string VerwijderBedrijfCheck { get; set; }
        [BindProperty]
        public string VerwijderPartCheck { get; set; }


        public async void OnGet()
        {
            //currentUserId = Request.Cookies["UserId"];
            //partOfBedrijf = Request.Cookies["PartOfBedrijf"];

            //if (partOfBedrijf == "Bedrijf")
            //{
            //    Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));

            //}

            //if (partOfBedrijf == "Particulier")
            //{
            //    Part = await autoStoreDatabase.VindParticulierOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));

            //}
            await LoadPage();
        }

        public async Task<ActionResult> OnPostWijzigBedrijf()
        {
            if (EmailInput == null)
            {
                Errormessage = "vul een emailadres in";
                await LoadPage();
                return Page();
            }
            if (WachtwoordInput == null)
            {
                Errormessage = "vul een emailadres in";
                await LoadPage();
                return Page();
            }
            if (await autoStoreDatabase.CheckBedrijfWachtwoord(EmailInput, WachtwoordInput))
            {
                Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
                if (NieuwWachtwoord == null)
                {
                    try
                    {
                        Bedrijf.UserName = UserNameInput;
                        Bedrijf.Wachtwoord = HasherMaker.ToSHA256(WachtwoordInput);
                        Bedrijf.Email = EmailInput;
                        Bedrijf.Adres = AdresInput;
                        Bedrijf.TelefoonNummer = TelefoonNummerInput;
                        Bedrijf.BetaalGegevens = BetaalGegevensInput;

                        await autoStoreDatabase.UpdateBedrijf(Bedrijf);
                        _logger.LogInformation("{Bedrijf} heeft zijn gegevens gewijzigd", Bedrijf.UserName);
                        return RedirectToPage("/BedrijfUserPagina");
                    }
                    catch (Exception ex)
                    {
                        Errormessage = ex.Message;
                        await LoadPage();//Er moet hier wel nog een interceptie bij dat als een klant gegevens per ongeluk verkeerd invult dit geen clutter creeert.
                        _logger.LogError("{Bedrijf} heeft geprobeerd zijn gegevens te gewijzigen, maar het is mislukt.{Error}", Bedrijf.UserName, Errormessage);
                        return Page();
                    }
                }
                if (NieuwWachtwoord != null)
                {
                    if (Validator.WachtwoordValidator(NieuwWachtwoord))
                    {
                        try
                        {

                            Bedrijf.UserName = UserNameInput;
                            Bedrijf.Wachtwoord = HasherMaker.ToSHA256(NieuwWachtwoord);
                            Bedrijf.Email = EmailInput;
                            Bedrijf.Adres = AdresInput;
                            Bedrijf.TelefoonNummer = TelefoonNummerInput;
                            Bedrijf.BetaalGegevens = BetaalGegevensInput;

                            await autoStoreDatabase.UpdateBedrijf(Bedrijf);
                            _logger.LogInformation("{Bedrijf} heeft zijn gegevens en zijn WACHTWOORD gewijzigd", Bedrijf.UserName);
                            return RedirectToPage("/BedrijfUserPagina");
                        }
                        catch (Exception ex)
                        {
                            Errormessage = ex.Message;
                            await LoadPage();
                            _logger.LogError("{Bedrijf} heeft geprobeerd zijn gegevens en zijn WACHTWOORD te gewijzigen, maar het is mislukt.{Error}", Bedrijf.UserName,Errormessage);
                            return Page();
                        }
                    }
                    else
                    {
                        await LoadPage();
                        Errormessage = "Je wachtwoord moet minstens 10 karakters lang zijn, een hoofdletter en een cijfer bevatten.";
                        return Page();
                    }
                }
            }
            await LoadPage();
            Errormessage = "Je moet je oude wachtwoord goed invullen.";
            return Page();
        }

        public async Task<ActionResult> OnPostWijzigParticulier()
        {
            if (EmailInput == null)
            {
                Errormessage = "Vul een emailadres in";
                await LoadPage();
                return Page();
            }
            if (WachtwoordInput == null)
            {
                Errormessage = "Vul een wachtwoord in";
                await LoadPage();
                return Page();
            }
            if (await autoStoreDatabase.CheckParticulierWachtwoord(EmailInput, WachtwoordInput))
            {
                Part = await autoStoreDatabase.VindParticulierOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
                if (NieuwWachtwoord == null)
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

                        await autoStoreDatabase.UpdateParticulier(Part);
                        _logger.LogInformation("{Particulier} heeft zijn gegevens gewijzigd", Part.UserName);
                        return RedirectToPage("/ParticulierUserPagina");
                    }
                    catch (Exception ex)
                    {
                        Errormessage = ex.Message;
                        await LoadPage();
                        _logger.LogError("{Particulier} heeft geprobeerd zijn gegevens, maar het is mislukt.{Error}", Part.UserName, Errormessage);
                        return Page();
                    }
                }

                if (NieuwWachtwoord != null) if (Validator.WachtwoordValidator(NieuwWachtwoord))
                    {
                        try
                        {
                            Part.UserName = UserNameInput;
                            Part.Wachtwoord = HasherMaker.ToSHA256(NieuwWachtwoord);
                            Part.Email = EmailInput;
                            Part.Adres = AdresInput;
                            Part.VoorNaam = VoorNaamInput;
                            Part.AchterNaam = AchterNaamInput;
                            Part.TelefoonNummer = TelefoonNummerInput;
                            Part.BetaalGegevens = BetaalGegevensInput;

                            await autoStoreDatabase.UpdateParticulier(Part);
                            _logger.LogInformation("{Particulier} heeft zijn gegevens en zijn WACHTWOORD gewijzigd", Part.UserName);
                            return RedirectToPage("/ParticulierUserPagina");
                        }
                        catch (Exception ex)
                        {
                            Errormessage = ex.Message;
                            await LoadPage();
                            _logger.LogError("{Particulier} heeft geprobeerd zijn gegevens en zijn WACHTWOORD te gewijzigen, maar het is mislukt.{Error}", Part.UserName, Errormessage);
                            return Page();
                        }
                    }
                    else
                    {
                        await LoadPage();
                        Errormessage = "Je wachtwoord moet minstens 10 karakters lang zijn, een hoofdletter en een cijfer bevatten.";
                        return Page();
                    }
            }

            await LoadPage();
            Errormessage = "Je moet je oude wachtwoord goed invullen.";
            return Page();
        }

        public async Task<ActionResult> OnPostVerwijderBedrijfWaarschuwing()
        {
            await LoadPage();
            return Page();
        }

        public async Task<ActionResult> OnPostVerwijderPartWaarschuwing()
        {
            await LoadPage();
            return Page();
        }

        public async Task<ActionResult> OnPostVerwijderBedrijf()
        {
            Bedrijf _bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
            await autoStoreDatabase.VerwijderBedrijf(_bedrijf.UserId);
            _logger.LogInformation("{Bedrijf} heeft zijn account verwijderd", _bedrijf.UserName);
            return RedirectToPage("/MainPage");
        }

        public async Task<ActionResult> OnPostVerwijderParticulier()
        {
            Particulier _particulier = await autoStoreDatabase.VindParticulierOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));
            await autoStoreDatabase.VerwijderParticulier(_particulier.UserId);
            _logger.LogInformation("{Particulier} heeft zijn account verwijderd", _particulier.UserName);
            return RedirectToPage("/MainPage");
        }

        public async Task LoadPage()
        {
            currentUserId = Request.Cookies["UserId"];
            partOfBedrijf = Request.Cookies["PartOfBedrijf"];

            if (partOfBedrijf == "Bedrijf")
            {
                Bedrijf = await autoStoreDatabase.VindBedrijfOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));

            }

            if (partOfBedrijf == "Particulier")
            {
                Part = await autoStoreDatabase.VindParticulierOpUserId(Convert.ToInt32(Request.Cookies["UserId"]));

            }

        }

    }
}
