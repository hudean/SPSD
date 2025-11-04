using Microsoft.EntityFrameworkCore;
using SPSD.WebApi.Model;

namespace SPSD.WebApi
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        #region DbSet

        public DbSet<BoardInfo> BoardInfos { get; set; }

        public DbSet<MaterialInfo> MaterialInfos { get; set; }

        public DbSet<MaterialItem> MaterialItems { get; set; }

        public DbSet<ProjectInfo> ProjectInfos { get; set; }

        public DbSet<WarbodyInfo> WarbodyInfos { get; set; }

        public DbSet<KineticEnergyInfo> KineticEnergyInfos { get; set; }

        public DbSet<KangBaoInfo> KangBaoInfos { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
