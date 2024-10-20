using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class ParticulierLoginModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        public ParticulierLoginModel(AutoStoreDatabase _autoStoreDatabase)
        {
            autoStoreDatabase = _autoStoreDatabase;
        }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string EmailInput { get; set; }
        [BindProperty]
        public string WachtwoordInput { get; set; }


        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (EmailInput != null && WachtwoordInput != null)
            {
                bool methodAnswer = await autoStoreDatabase.CheckParticulierWachtwoord(EmailInput, WachtwoordInput);
                if (methodAnswer)
                {
                    Particulier particulier = await autoStoreDatabase.VindParticulierOpEmail(EmailInput);
                    Response.Cookies.Append("UserId", Convert.ToString(particulier.UserId));
                    Response.Cookies.Append("PartOfBedrijf", "Particulier");
                    Message = "Inloggen is gelukt. Waarom ben je nog hier?";
                    return RedirectToPage("/ParticulierUserPagina");
                }
                else { Message = "inloggen heeft gefaald."; return Page(); }
            }
            else { Message = "inloggen heeft gefaald."; return Page(); }
        }
    }
}
