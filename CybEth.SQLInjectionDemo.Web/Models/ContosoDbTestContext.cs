using Microsoft.EntityFrameworkCore;

namespace CybEth.SQLInjectionDemo.Web.Models;

public partial class ContosoDbTestContext : DbContext
{
    public ContosoDbTestContext()
    {
    }

    public ContosoDbTestContext(DbContextOptions<ContosoDbTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=192.168.56.5;Database=ContosoDB_Test;User Id=dbaccess;Password=P@ssw0rd;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Persons");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
