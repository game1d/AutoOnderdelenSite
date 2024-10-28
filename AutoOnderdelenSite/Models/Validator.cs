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

        public static bool ScoreValidator(int score)
        {
            bool result = false;
            if (score >= 0 && score <= 10) { result = true; }
            return result;
        }

        public static string BedragValidator(string bedrag)
        {
            string result = "";
            var hasNumber = new Regex(@"[0-9]+");
            if (!hasNumber.IsMatch(bedrag))
            {
                result = "Vul alleen nummers in.";
                return result;
            }
            var NoDot = new Regex(@"[.]");
            if (NoDot.IsMatch(bedrag))
            {
                result = "gebruik komma's geen punten.";
                return result;
            }
            double bedragInNummers = Convert.ToDouble(bedrag);
            bedragInNummers = bedragInNummers * 100;
            if (bedragInNummers % 1 != 0)
            {
                result = " geef maar 2 cijfers achter de komma.";
                return result;
            }

            return result;
        }

    }
}
