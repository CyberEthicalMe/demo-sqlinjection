using Microsoft.EntityFrameworkCore;

namespace CybEth.SQLInjectionDemo.Web.Models;

public partial class ContosoDbTestContext : DbContext
{
    private readonly string _connectionString;
    public ContosoDbTestContext(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public ContosoDbTestContext(DbContextOptions<ContosoDbTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(this._connectionString);
    }

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
