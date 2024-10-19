using AutoOnderdelenSite.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoOnderdelenSite.Data
{
    public class AutoDbContext : DbContext
    {
        public AutoDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Particulier> ParticulierDb { get; set; }
        public DbSet<Bedrijf> BedrijfDb { get; set; }
        public DbSet<TweedeHandsAdvertentie> TweedeHandsAdvertentieDb { get; set; }
        public DbSet<RefurbishedAdvertentie> RefurbishedAdvertentieDb { get; set; }
        public DbSet<NieuwProductAdvertentie> NieuwProductAdvertentieDb { get; set; }
        public DbSet<Product> ProductenDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Particulier>().HasData(
                new Particulier { UserId = 1, UserName = "Bertus175", Wachtwoord = "Wachtwoord1", VoorNaam = "Bert", AchterNaam = "Havik", Adres = "Paterstraat 12", Email = "Berthavik@sox.nl", TelefoonNummer = "06-735921864", BetaalGegevens = "" },
                new Particulier { UserId = 2, UserName = "Ziep87", Wachtwoord = "Password2", VoorNaam = "Joep", AchterNaam = "Stoeptegel", Adres = "Havenlaan 38", Email = "Joepstoeptegel@sox.nl", TelefoonNummer = "06-243951189" }
                );

            modelBuilder.Entity<Bedrijf>().HasData(
                new Bedrijf { UserId = 1, UserName = "Poel", Wachtwoord = "Poel1", Adres = "Industrielaan 145", TelefoonNummer = "046-53622815", Email = "Poelinkopen@bisness.com", BetaalGegevens="blubank 123907534" },
                new Bedrijf { UserId = 2, UserName = "Merdeces", Wachtwoord = "Merdeces!", Adres = "Moterweg 71", TelefoonNummer = "046-7469582", Email = "Merdecesinkopen@bisness.com", BetaalGegevens = "blubank 46321897" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Poel wiel", ProductType = "Wiel", Omschrijving = "Een wiel van het merk Poel" },
                new Product { ProductId = 2, ProductName = "Merdeces wiel", ProductType = "Wiel", Omschrijving = "Een wiel van het merk Merdeces" },
                new Product { ProductId = 3, ProductName = "Poel ruitenwisser", ProductType = "Ruitenwisser", Omschrijving = "Een ruitenwisser van het merk Poel" },
                new Product { ProductId = 4, ProductName = "Merdeces ruitenwisser", ProductType = "Ruitenwisser", Omschrijving = "Een ruitenwisser van het merk Merdeces" },
                new Product { ProductId = 5, ProductName = "Nitroen knalpijp", ProductType = "Knalpijp", Omschrijving = "Een knalpijp van het merk Nitroen" },
                new Product { ProductId = 6, ProductName = "Poel stoel", ProductType = "Stoel", Omschrijving = "Een stoel van het merk Nitroen" }
                );

            modelBuilder.Entity<TweedeHandsAdvertentie>().HasData(
                new TweedeHandsAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 1, StaatProduct = "Poel wiel 1 jaar gebruikt." },
                new TweedeHandsAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 2, StaatProduct = "Merdeces wiel 1 jaar gebruikt." },
                new TweedeHandsAdvertentie { AdvertentieId = 3, UserId = 1, ProductId = 5, StaatProduct = "Nitroen knalpijp bijna niet gebruikt." }
                );

            modelBuilder.Entity<RefurbishedAdvertentie>().HasData(
                new RefurbishedAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 6, StaatProduct = "Refurbished Poel stoel" },
                new RefurbishedAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 4, StaatProduct = "Refurbished Merdeces ruitenwisser" }
                );

            modelBuilder.Entity<NieuwProductAdvertentie>().HasData(
                new NieuwProductAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 1, Aantal = 65 },
                new NieuwProductAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 2, Aantal = 43 }
                );


        }

    }
}
