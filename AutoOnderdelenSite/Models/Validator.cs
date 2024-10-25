using System.Globalization;
using System.Text.RegularExpressions;

namespace AutoOnderdelenSite.Models
{
    public static class Validator
    {

        public static bool WachtwoordValidator(string wachtwoord)
        {
            bool result = false;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            if (wachtwoord.Length > 10)
            {
                if (hasNumber.IsMatch(wachtwoord))
                {
                    if (hasUpperChar.IsMatch(wachtwoord))
                    { result = true; }
                }
            }
            return result;
        }


    }
}
