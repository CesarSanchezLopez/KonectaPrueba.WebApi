using KonectaPrueba.Models;
using Microsoft.EntityFrameworkCore;

namespace KonectaPrueba.WebApi.Repository
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }


        public virtual DbSet<User>? UserInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=KonectaDB;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("UserInfo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CreatedDate).IsUnicode(false);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
