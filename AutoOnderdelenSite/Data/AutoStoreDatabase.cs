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

        public async Task<List<Particulier>> GetParticulierenAsync()
        {
            return DataBase.ParticulierDb.Include(s => s.TweedeHandsAdvertenties).ThenInclude(r=>r.Product).ToList();
        }
        public async Task<List<Bedrijf>> GetBedrijvenAsync()
        {
            return DataBase.BedrijfDb.Include(s => s.RefurbishedAvertenties).ThenInclude(q=>q.Product)
                                    .Include(t => t.NieuwProductAdvertenties).ThenInclude(u => u.Product).ToList();
        }
        public async Task<List<TweedeHandsAdvertentie>> GetTweedeHandsAdvertentiesAsync()
        {
            return DataBase.TweedeHandsAdvertentieDb.Include(s => s.Product).ToList();
        }
        public async Task<List<RefurbishedAdvertentie>> GetRefurbishedAdvertentiesAsync()
        {
            return DataBase.RefurbishedAdvertentieDb.Include(s => s.Product).ToList();
        }
        public async Task<List<NieuwProductAdvertentie>> GetNieuweProductAdvertentiesAsync()
        {
            return DataBase.NieuwProductAdvertentieDb.Include(s=>s.Product).ToList();
        }
        public async Task<List<Product>> GetAlleProductenAsync()
        {
            return DataBase.ProductenDb.ToList();
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
            List<Bedrijf> bedrijvenlijst = await GetBedrijvenAsync();//Dit is niet ideaal gezien je extra vermogen nodig hebt en de gegevens zijn vaker aanwezig dus meer steelmogelijkhden voor hackers.

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
            List<Bedrijf> bedrijven = await GetBedrijvenAsync();
            foreach (Bedrijf bedrijf1 in bedrijven)
            {
                if (bedrijf1.UserId == UserId) { bedrijf = bedrijf1; }
            }
            return bedrijf;
        }

        public async Task<Particulier> VindParticulierOpEmail(string emailAdres)
        {
            Particulier particulier = new Particulier();
            List<Particulier> particulierenlijst = await GetParticulierenAsync();//Dit is niet ideaal gezien je extra vermogen nodig hebt en de gegevens zijn vaker aanwezig dus meer steelmogelijkhden voor hackers.

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
            List<Particulier> particulieren= await GetParticulierenAsync();
            List<Particulier> particulierenlijst = await GetParticulierenAsync();

            foreach (Particulier _particulier in particulierenlijst)
            {
                if (_particulier.UserId == UserId)
                { particulier = _particulier; }
            }
            if (particulier != null) { return particulier; }
            else { throw new Exception("Particulier niet gevonden"); }
        }

        public async Task<bool> CheckParticulierWachtwoord(string emailAdres, string wachtWoord)
        {
            bool _result = false;
            Particulier _particulier = await VindParticulierOpEmail(emailAdres);
            if (_particulier != null)
            {
                if (_particulier.Wachtwoord == HasherMaker.ToSHA256(wachtWoord))
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
            if (_bedrijf != null)
            {
                if (_bedrijf.Wachtwoord == HasherMaker.ToSHA256(wachtWoord))
                {
                    _result = true;
                }
            }
            return _result;
        }

        public async Task VoegProductToe(Product _nieuwproduct)
        {
            DataBase.Add(_nieuwproduct);
            await DataBase.SaveChangesAsync();
        }

        public async Task<NieuwProductAdvertentie> GetNieuwAdvertentie(int nieuwProdId)
        {
            NieuwProductAdvertentie result = new NieuwProductAdvertentie();
            var adList = await GetNieuweProductAdvertentiesAsync();
            foreach (var ad in adList) 
            { 
            if(ad.AdvertentieId == nieuwProdId) 
                { result=ad; }
            }
            return result;
        }

        public async Task<RefurbishedAdvertentie> GetRefurbishedAdvertentie(int refurId)
        {
            RefurbishedAdvertentie result = new RefurbishedAdvertentie();
            var adList = await GetRefurbishedAdvertentiesAsync();
            foreach (var ad in adList)
            {
                if (ad.AdvertentieId == refurId)
                { result = ad; }
            }
            return result;
        }

        public async Task<TweedeHandsAdvertentie> GetTweedeHandsAdvertentie(int tweedeHandsId)
        {
            TweedeHandsAdvertentie result = new TweedeHandsAdvertentie();
            var adList = await GetTweedeHandsAdvertentiesAsync();
            foreach (var ad in adList)
            {
                if (ad.AdvertentieId == tweedeHandsId)
                { result = ad; }
            }

            return result;
        }

        public async Task VoegNieuwAdvertentieToe(NieuwProductAdvertentie nieuwProductAdvertentie)
        {
            DataBase.Add(nieuwProductAdvertentie);
            await DataBase.SaveChangesAsync();
        }

        public async Task VoegRefurbishedAdvertentieToe(RefurbishedAdvertentie refurbishedAdvertentie)
        {
            DataBase.Add(refurbishedAdvertentie);
            await DataBase.SaveChangesAsync();
        }

        public async Task VoegTweedeHandsAdvertentieToe(TweedeHandsAdvertentie tweedeHandsAdvertentie)
        {
            DataBase.Add(tweedeHandsAdvertentie);
            await DataBase.SaveChangesAsync();
        }
        public async Task VerwijderNieuwAdvertentie(int advId)
        {
            NieuwProductAdvertentie nieuwProductAdvertentie= await GetNieuwAdvertentie(advId);
            DataBase.NieuwProductAdvertentieDb.Remove(nieuwProductAdvertentie);
            await DataBase.SaveChangesAsync();
        }

        public async Task VerwijderRefurbishedAdvertentie(int advId)
        {
            RefurbishedAdvertentie refurbishedAdvertentie = await GetRefurbishedAdvertentie(advId);
            DataBase.RefurbishedAdvertentieDb.Remove(refurbishedAdvertentie);
            await DataBase.SaveChangesAsync();
        }

        public async Task VerwijderTweedeHandsAdvertentie(int advId)
        {
            TweedeHandsAdvertentie tweedeHandsAdvertentie= await GetTweedeHandsAdvertentie(advId);
            DataBase.TweedeHandsAdvertentieDb.Remove(tweedeHandsAdvertentie);
            await DataBase.SaveChangesAsync();
        }

        public async Task MaakBieding(Bieding _bieding)
        {
            DataBase.Add(_bieding);
            await DataBase.SaveChangesAsync();
        }

        public async Task MaakRefurKoop(KoopRefurbished _refurkoop)
        {
            DataBase.RefurKoopDb.Add(_refurkoop);
            await DataBase.SaveChangesAsync();
        }
        public async Task MaakNieuwKoop(NieuwKoop _nieuwKoop)
        {
            DataBase.NieuwKoopDb.Add(_nieuwKoop);
            await DataBase.SaveChangesAsync();
        }
        public async Task<List<WikiArtikelBedrijf>>GetWikiArtikelBedrijfsAsync(int wikiId)
        {
            return DataBase.WikiArtikelBedrijfsDb.Include(p => p.Product).ToList();
        }
        public async Task<List<WikiArtikelParticulier>> GetArtikelParticuliersAsync()
        {
            return DataBase.WikiArtikelParticuliersDb.Include(p => p.Product).ToList();
        }
        public async Task<List<ProductWikiVerzameling>>ProductWikiVerzamelingsAsync()
        {
            return DataBase.ProductWikiVerzameling.Include(p => p.Product).ToList();
        }

        public async Task ToevoegenWikiArtikelBedrijf(WikiArtikelBedrijf wikiArtikel)
        {
            DataBase.Add(wikiArtikel);
            await DataBase.SaveChangesAsync();
        }
        public async Task ToevoegenWikiArtikelParticulier(WikiArtikelParticulier wikiArtikel)
        {
            DataBase.Add(wikiArtikel);
            await DataBase.SaveChangesAsync();
        }
       /* public async Task VerwijderWikiArtikelBedrijf(int WikiId)
        {
            WikiArtikelBedrijf wikiArtikelBedrijf = await GetWikiArtikelBedrijfsAsync(WikiId);
            DataBase.WikiArtikelBedrijfsDb.Remove(wikiArtikelBedrijf);
            await DataBase.SaveChangesAsync();
        }
        public async Task VerwijderWikiArtikelParticulier(int WikiId)
        {
            WikiArtikelParticulier wikiArtikelParticulier = await GetWikiArtikelParticulier(WikiId);
            DataBase.WikiArtikelParticuliersDb.Remove(wikiArtikelParticulier);
            await DataBase.SaveChangesAsync();
        }*/
        
        
    }
}
