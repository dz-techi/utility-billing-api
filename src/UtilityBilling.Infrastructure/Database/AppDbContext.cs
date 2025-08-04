using Microsoft.EntityFrameworkCore;
using UtilityBilling.Domain.Models;

namespace UtilityBilling.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    // Define DbSets for the entities
    public DbSet<Property> Properties { get; set; }
    public DbSet<UtilityType> UtilityTypes { get; set; }
    public DbSet<UtilityBillPeriod> UtilityBillPeriods { get; set; }
    public DbSet<UtilityBill> UtilityBills { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PropertyUser> PropertyUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Property>()
            .HasMany(p => p.UtilityTypes)
            .WithOne(u => u.Property)
            .HasForeignKey(u => u.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Property>()
            .HasMany(p => p.UtilityBillPeriods)
            .WithOne(u => u.Property)
            .HasForeignKey(u => u.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    
        modelBuilder.Entity<Property>()
            .HasMany(p => p.PropertyUsers)
            .WithOne(pu => pu.Property)
            .HasForeignKey(pu => pu.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.PropertyUsers)
            .WithOne(pu => pu.User)
            .HasForeignKey(pu => pu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UtilityBillPeriod>()
            .HasMany(u => u.UtilityBills)
            .WithOne(u => u.UtilityBillPeriod)
            .HasForeignKey(u => u.UtilityBillPeriodId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}