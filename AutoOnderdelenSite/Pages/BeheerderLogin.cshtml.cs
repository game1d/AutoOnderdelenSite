using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BeheerderLoginModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public BeheerderLoginModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string EmailInput { get; set; }
        [BindProperty]
        public string WachtwoordInput { get; set; }


        public async void OnGet()
        {

        }

        public async Task<ActionResult> OnPost()
        {
            if (EmailInput != null && WachtwoordInput != null)
            {
                bool methodAnswer = await autoStoreDatabase.CheckBeheerderWachtwoord(EmailInput, WachtwoordInput);
                if (methodAnswer)
                {
                    Beheerder beheerder = await autoStoreDatabase.VindBeheerderOpEmail(EmailInput);
                    Response.Cookies.Append("UserId", Convert.ToString(beheerder.UserId));
                    Response.Cookies.Append("PartOfBedrijf", "Beheerder");
                    Message = "Inloggen is gelukt. Waarom ben je nog hier?";
                    return RedirectToPage("/BeheerderPagina");
                }
                else { Message = "inloggen heeft gefaald."; return Page(); }
            }
            else { Message = "inloggen heeft gefaald."; return Page(); }
        }
    }
}
