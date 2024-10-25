using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AutoOnderdelenSite.Models
{
    [Index(nameof(Email),IsUnique =true)]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Wachtwoord { get; set; }
        
        public string Email { get; set; }

        public string? BanStatus {  get; set; }
    }
}
