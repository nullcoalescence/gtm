using gtm.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace gtm.Db
{
    public class GtmContext : DbContext
    {
        public DbSet<Deck> Decks { get; set; }

        public string DbPath { get; set; }

        public GtmContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "gtm.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
