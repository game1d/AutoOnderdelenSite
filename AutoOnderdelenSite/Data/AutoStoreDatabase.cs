using AutoOnderdelenSite.Models;
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
            return  DataBase.ParticulierDb.Include(s=>s.TweedeHandsAdvertenties).ToList();

        }
        public async Task<List<TweedeHandsAdvertentie>> GetTweedeHandsAdvertentiesAsync()
        {
            return DataBase.TweedeHandsAdvertentieDb.ToList();
        }

    }
}
