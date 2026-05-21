using Cwiczenia10.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PC>(entity =>
        {
            entity.ToTable("PCs");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Weight)
                .IsRequired();

            entity.Property(e => e.Warranty)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.Stock)
                .IsRequired();
        });
        
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.ToTable("ComponentManufacturers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.FoundationDate)
                .HasColumnType("date")
                .IsRequired();
        });
        
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentTypes");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
        });
        
        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components");

            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.Description)
                .IsRequired();

            entity.HasOne(e => e.Manufacturer)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Type)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.ToTable("PCComponents");

            entity.HasKey(e => new { e.PCId, e.ComponentCode });

            entity.Property(e => e.ComponentCode)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.HasOne(e => e.PC)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateTime(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateTime(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateTime(1994, 1, 1)
            }
        );
        
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );
        
        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ComponentManufacturersId = 3,
                ComponentTypesId = 3
            }
        );
        
        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new PC
            {
                Id = 3,
                Name = "Creator Workstation",
                Weight = 15.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 10, 10, 15, 0),
                Stock = 3
            }
        );
        
        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "GPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "RAM0000001",
                Amount = 2
            }
        );
    }
}