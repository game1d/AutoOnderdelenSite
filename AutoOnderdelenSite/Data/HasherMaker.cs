using System.Security.Cryptography;
using System.Text;

namespace AutoOnderdelenSite.Data
{
    public static class HasherMaker
    {
        public static string ToSHA256(string _string)//Vond deze op youtube. Ik neem aan dat deze oud en dus lek is, maar voor deze site gaat dat niet uitmaken.
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_string));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) 
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
