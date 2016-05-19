using ProjektHermods.Models;
using System.Data.Entity;

namespace ProjektHermods
{
    public class ReceptTipsContext : DbContext
    {
        public ReceptTipsContext() : base("rece") { }

        public DbSet<Ingrediens> Ingrediens { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<ChoosenType> ChoosenTypes { get; set; }

        public System.Data.Entity.DbSet<ProjektHermods.Models.SearchResultList> SearchResultLists { get; set; }
        }
}