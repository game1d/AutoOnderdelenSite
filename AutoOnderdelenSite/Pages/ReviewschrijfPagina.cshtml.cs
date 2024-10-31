using AutoOnderdelenSite.Data;
using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoOnderdelenSite.Pages
{
    public class ReviewschrijfPaginaModel : PageModel
    {

        private readonly AutoStoreDatabase autoStoreDatabase;

        private readonly ILogger<ReviewschrijfPaginaModel> _logger;

        public ReviewschrijfPaginaModel(AutoStoreDatabase _autoStoreDatabase, ILogger<ReviewschrijfPaginaModel> logger)
        {
            autoStoreDatabase = _autoStoreDatabase;
            _logger = logger;
        }

        [BindProperty]
        public string ReviewSoort { get; set; }
        [BindProperty]
        public Particulier Reviewer { get; set; }
        [BindProperty]
        public Particulier GereviewdePart { get; set; }
        [BindProperty]
        public Bedrijf GereviewdeBedrijf { get; set; }

        [BindProperty]
        public List<Product> products { get; set; }
        [BindProperty]
        public int ProductIdInput { get; set; }
        [BindProperty]
        public int ReviewerIdInput { get; set; }
        [BindProperty]
        public int GereviewdeInput { get; set; }
        [BindProperty]
        public string ReviewerNameInput { get; set; }
        [BindProperty]
        public int ScoreInput { get; set; }
        [BindProperty]
        public string OmschrijvingInput { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public async void OnGet(string reviewSoort, int GereviewdeId, int reviewerId)
        {
            if (reviewSoort == "Particulier")
            {
                products = await autoStoreDatabase.GetAlleProductenAsync();
                ReviewSoort = reviewSoort;
                GereviewdePart = await autoStoreDatabase.VindParticulierOpUserId(GereviewdeId);
                Reviewer = await autoStoreDatabase.VindParticulierOpUserId(reviewerId);
            }


            if (reviewSoort == "Bedrijf")
            {
                products = await autoStoreDatabase.GetAlleProductenAsync();
                ReviewSoort = reviewSoort;
                GereviewdeBedrijf = await autoStoreDatabase.VindBedrijfOpUserId(GereviewdeId);
                Reviewer = await autoStoreDatabase.VindParticulierOpUserId(reviewerId);

            }
        }

        public async Task<ActionResult> OnPostReviewParticulier()
        {

            ParticulierReview _review = new ParticulierReview();
            _review.reviewerId = ReviewerIdInput;
            _review.GereviewdeId = GereviewdeInput;
            _review.ProductId = ProductIdInput;
            _review.ReviewerName = ReviewerNameInput;
            _review.Beschrijving = OmschrijvingInput;
            if (!Validator.ScoreValidator(ScoreInput))
            {
                Message = "Geef een score tussen 0 en 10";
                ReviewSoort = "Particulier";
                GereviewdePart = await autoStoreDatabase.VindParticulierOpUserId(_review.GereviewdeId);
                Reviewer = await autoStoreDatabase.VindParticulierOpUserId(_review.reviewerId);
                products = await autoStoreDatabase.GetAlleProductenAsync();
                return Page();
            }
            else
            {
                _review.Score = ScoreInput;

                await autoStoreDatabase.MaakPartReview(_review);

                _logger.LogInformation("{Particulier} heeft een review geschreven over particulier {ReviewOnderwerp}.", ReviewerNameInput, GereviewdeInput);

                return RedirectToPage("/ParticulierUserPagina");
            }
        }

        public async Task<ActionResult> OnPostReviewBedrijf()
        {
            BedrijfReview _review = new BedrijfReview();
            _review.reviewerId = ReviewerIdInput;
            _review.GereviewdeId = GereviewdeInput;
            _review.ProductId = ProductIdInput;
            _review.ReviewerName = ReviewerNameInput;
            _review.Beschrijving = OmschrijvingInput;
            if (!Validator.ScoreValidator(ScoreInput))
            {
                Message = "Geef een score tussen 0 en 10";
                ReviewSoort = "Bedrijf";
                GereviewdeBedrijf = await autoStoreDatabase.VindBedrijfOpUserId(_review.GereviewdeId);
                Reviewer = await autoStoreDatabase.VindParticulierOpUserId(_review.reviewerId);
                products = await autoStoreDatabase.GetAlleProductenAsync();
                return Page();
            }
            else
            {
                _review.Score = ScoreInput;

                await autoStoreDatabase.MaakBedrijfReview(_review);

                _logger.LogInformation("{Particulier} heeft een review gecshreven over bedrijf {ReviewOnderwerp}.", ReviewerNameInput, GereviewdeInput);

                return RedirectToPage("/ParticulierUserPagina");
            }
        }
    }
}
