﻿using AutoOnderdelenSite.Models;
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
        public DbSet<Bieding> BiedingDb { get; set; }
        public DbSet<KoopRefurbished> RefurKoopDb { get; set; }
        public DbSet<NieuwKoop> NieuwKoopDb { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Particulier>().HasData(
                new Particulier { UserId = 1, UserName = "Bertus175", Wachtwoord = HasherMaker.ToSHA256("Wachtwoord1"), VoorNaam = "Bert", AchterNaam = "Havik", Adres = "Paterstraat 12", Email = "Berthavik@sox.nl", TelefoonNummer = "06-735921864", BetaalGegevens = "GNI 2154846" },
                new Particulier { UserId = 2, UserName = "Ziep87", Wachtwoord = HasherMaker.ToSHA256("Password2"), VoorNaam = "Joep", AchterNaam = "Stoeptegel", Adres = "Havenlaan 38", Email = "Joepstoeptegel@sox.nl", TelefoonNummer = "06-243951189" }
                );

            modelBuilder.Entity<Bedrijf>().HasData(
                new Bedrijf { UserId = 1, UserName = "Poel", Wachtwoord = HasherMaker.ToSHA256("Poel1"), Adres = "Industrielaan 145", TelefoonNummer = "046-53622815", Email = "Poelinkopen@bisness.com", BetaalGegevens="blubank 123907534" },
                new Bedrijf { UserId = 2, UserName = "Merdeces", Wachtwoord = HasherMaker.ToSHA256("Merdeces!"), Adres = "Moterweg 71", TelefoonNummer = "046-7469582", Email = "Merdecesinkopen@bisness.com", BetaalGegevens = "blubank 46321897" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductNaam = "Poel wiel", ProductType = "Wiel", Omschrijving = "Een wiel van het merk Poel" },
                new Product { ProductId = 2, ProductNaam = "Merdeces wiel", ProductType = "Wiel", Omschrijving = "Een wiel van het merk Merdeces" },
                new Product { ProductId = 3, ProductNaam = "Poel ruitenwisser", ProductType = "Ruitenwisser", Omschrijving = "Een ruitenwisser van het merk Poel" },
                new Product { ProductId = 4, ProductNaam = "Merdeces ruitenwisser", ProductType = "Ruitenwisser", Omschrijving = "Een ruitenwisser van het merk Merdeces" },
                new Product { ProductId = 5, ProductNaam = "Nitroen knalpijp", ProductType = "Knalpijp", Omschrijving = "Een knalpijp van het merk Nitroen" },
                new Product { ProductId = 6, ProductNaam = "Poel stoel", ProductType = "Stoel", Omschrijving = "Een stoel van het merk Nitroen" }
                );

            modelBuilder.Entity<TweedeHandsAdvertentie>().HasData(
                new TweedeHandsAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 1, StaatProduct = "Poel wiel 1 jaar gebruikt.", Prijs=30.00 },
                new TweedeHandsAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 2, StaatProduct = "Merdeces wiel 1 jaar gebruikt.", Prijs=30.00 },
                new TweedeHandsAdvertentie { AdvertentieId = 3, UserId = 1, ProductId = 5, StaatProduct = "Nitroen knalpijp bijna niet gebruikt.", Prijs=29.00 }
                );

            modelBuilder.Entity<RefurbishedAdvertentie>().HasData(
                new RefurbishedAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 6, StaatProduct = "Refurbished Poel stoel",Prijs=200.00},
                new RefurbishedAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 4, StaatProduct = "Refurbished Merdeces ruitenwisser",Prijs=25.00 }
                );

            modelBuilder.Entity<NieuwProductAdvertentie>().HasData(
                new NieuwProductAdvertentie { AdvertentieId = 1, UserId = 1, ProductId = 1, Aantal = 65,Prijs=35.00 },
                new NieuwProductAdvertentie { AdvertentieId = 2, UserId = 2, ProductId = 2, Aantal = 43,Prijs=35.00 }
                );


        }

    }
}
