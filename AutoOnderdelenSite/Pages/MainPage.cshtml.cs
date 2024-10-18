using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class MainPageModel : PageModel
    {
        private readonly ILogger<MainPageModel> _logger;

        public MainPageModel(ILogger<MainPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
