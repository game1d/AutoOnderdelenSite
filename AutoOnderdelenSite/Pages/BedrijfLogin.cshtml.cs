using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class BedrijfLoginModel : PageModel
    {
        private readonly AutoStoreDatabase autoStoreDatabase;

        public BedrijfLoginModel(AutoStoreDatabase _autoStoreDatabase)
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
                bool methodAnswer = await autoStoreDatabase.CheckBedrijfWachtwoord(EmailInput, WachtwoordInput);
                if (methodAnswer)
                {
                    Bedrijf bedrijf = await autoStoreDatabase.VindBedrijfOpEmail(EmailInput);
                    Response.Cookies.Append("UserId", Convert.ToString(bedrijf.UserId));
                    Response.Cookies.Append("PartOfBedrijf", "Bedrijf");
                    Message = "Inloggen is gelukt. Waarom ben je nog hier?";
                    return RedirectToPage("/BedrijfUserPagina");
                }
                else { Message = "inloggen heeft gefaald."; return Page(); }
            }
            else { Message = "inloggen heeft gefaald."; return Page(); }
        }
    }
}
