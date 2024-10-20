using AutoOnderdelenSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoOnderdelenSite.Data
{
    public class AutoStoreDatabase
    {
        AutoDbContext DataBase;

        public AutoStoreDatabase(AutoDbContext dataBaseContext)
        {
            DataBase = dataBaseContext;
        }

        public async Task<List<Particulier>> GetParticulierenAsnyc()
        {
            return DataBase.ParticulierDb.Include(s => s.TweedeHandsAdvertenties).ToList();
        }
        public async Task<List<Bedrijf>> GetBedrijvenAsnyc()
        {
            return DataBase.BedrijfDb.Include(s => s.RefurbishedAvertenties).Include(t => t.NieuwProductAdvertenties).ToList();
        }
        public async Task<List<TweedeHandsAdvertentie>> GetTweedeHandsAdvertentiesAsync()
        {
            return DataBase.TweedeHandsAdvertentieDb.ToList();
        }

        public async Task ToevoegenParticulier(Particulier particulier)
        {
            DataBase.Add(particulier);
            await DataBase.SaveChangesAsync();
        }

        public async Task ToevoegenBedrijf(Bedrijf bedrijf)
        {
            DataBase.Add(bedrijf);
            await DataBase.SaveChangesAsync();
        }

        public async Task<Bedrijf> VindBedrijfOpEmail(string emailAdres)
        {
            Bedrijf bedrijf = new Bedrijf();
            List<Bedrijf> bedrijvenlijst = await GetBedrijvenAsnyc();//Dit is niet ideaal gezien je extra vermogen nodig hebt en de gegevens zijn vaker aanwezig dus meer steelmogelijkhden voor hackers.

            foreach (Bedrijf _bedrijf in bedrijvenlijst)
            {
                if (_bedrijf.Email == emailAdres)
                { bedrijf = _bedrijf; }
            }
            if (bedrijf != null) { return bedrijf; }
            else { throw new Exception("Bedrijf niet gevonden"); }//Beter om specifieke exceptions te maken, maar dat doe ik als ik voldoende tijd heb.
        }
        public async Task<Bedrijf> VindBedrijfOpUserId(int UserId)
        {
            Bedrijf bedrijf = new Bedrijf();
            bedrijf = DataBase.BedrijfDb.Find(UserId);
            return bedrijf;
        }

        public async Task<Particulier> VindParticulierOpEmail(string emailAdres)
        {
            Particulier particulier = new Particulier();
            List<Particulier> particulierenlijst = await GetParticulierenAsnyc();//Dit is niet ideaal gezien je extra vermogen nodig hebt en de gegevens zijn vaker aanwezig dus meer steelmogelijkhden voor hackers.

            foreach (Particulier _particulier in particulierenlijst)
            {
                if (_particulier.Email == emailAdres)
                { particulier = _particulier; }
            }
            if (particulier != null) { return particulier; }
            else { throw new Exception("Particulier niet gevonden"); }//Beter om specifieke exceptions te maken, maar dat doe ik als ik voldoende tijd heb.
        }
        public async Task<Particulier> VindParticulierOpUserId(int UserId)
        {
            Particulier particulier = new Particulier();
            particulier = DataBase.ParticulierDb.Find(UserId);
            return particulier;
        }

        public async Task<bool> CheckParticulierWachtwoord(string emailAdres, string wachtWoord)
        {
            bool _result = false;
            Particulier _particulier = await VindParticulierOpEmail(emailAdres);
            if (_particulier != null)
            {
                if (_particulier.Wachtwoord == wachtWoord)
                {
                    _result = true;
                }
            }
            return _result;
        }

        public async Task<bool> CheckBedrijfWachtwoord(string emailAdres, string wachtWoord)
        {
            bool _result = false;
            Bedrijf _bedrijf = await VindBedrijfOpEmail(emailAdres);//Ik zou liever direct zoeken op email maar hij wil op een int zoeken.
            if (_bedrijf !=null)
            {
                if (_bedrijf.Wachtwoord == wachtWoord)
                {
                    _result = true;
                }
            }
            return _result;
        }

    }
}
