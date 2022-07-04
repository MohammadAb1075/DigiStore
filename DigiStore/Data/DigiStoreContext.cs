using DigiStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigiStore.Data
{
    public class DigiStoreContext: DbContext
    {

        public DigiStoreContext() { }

        public DigiStoreContext(DbContextOptions<DigiStoreContext> options):base(options) { }
       
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=.;initial catalog=DigiStoreContext;integrated security=True;multipleactiveresultsets=True;application name=DigiStoreCodeFirst");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                      .HasName("PK_Category");

                entity.Property(e => e.CategoryName)
                      .IsRequired()
                      .HasMaxLength(128);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                      .HasName("PK_Product");

                entity.Property(e => e.ProductName)
                      .HasMaxLength(128);

                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Products_Categories");
            });
            //base.OnModelCreating(modelBuilder);
            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
