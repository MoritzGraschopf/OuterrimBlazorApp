using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context;

public class OuterRimContext(DbContextOptions<OuterRimContext> options) : DbContext(options)
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<AircraftCrew> AircraftCrews { get; set; }
    public DbSet<AircraftSpecification> AircraftSpezifications { get; set; }
    public DbSet<Compartment> Compartments { get; set; }
    public DbSet<CrimeSyndicate> CrimeSyndicates { get; set; }
    public DbSet<EnergySystem> EnergySystems { get; set; }
    public DbSet<EnviromentalSystem> EnviromentalSystems { get; set; }
    public DbSet<Machinery> Machineries { get; set; }
    public DbSet<Mercenary> Mercenaries { get; set; }
    public DbSet<MercenaryReputation> MercenaryReputations { get; set; }
    public DbSet<Weapon> Weapons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Aircraft>()
            .HasOne<AircraftSpecification>(a => a.AircraftSpecification)
            .WithMany(a => a.Aircrafts)
            .HasForeignKey(a => a.SpecificationId);

        builder.Entity<AircraftCrew>()
            .HasOne<Aircraft>(ac => ac.Aircraft)
            .WithMany(a => a.AircraftCrews)
            .HasForeignKey(ac => ac.AircraftId);
        builder.Entity<AircraftCrew>()
            .HasOne<Mercenary>(ac => ac.Mercenary)
            .WithMany(m => m.AircraftCrews)
            .HasForeignKey(ac => ac.MercenaryId);

        builder.Entity<MercenaryReputation>()
            .HasOne<Mercenary>(mr => mr.Mercenary)
            .WithMany(m => m.MercenaryReputations)
            .HasForeignKey(mr => mr.MercenaryId);
        builder.Entity<MercenaryReputation>()
            .HasOne<CrimeSyndicate>(cs => cs.CrimeSyndicate)
            .WithMany(m => m.MercenaryReputations)
            .HasForeignKey(cs => cs.SyndicateId);

        builder.Entity<Compartment>()
            .HasOne<Aircraft>(c => c.Aircraft)
            .WithMany(a => a.Compartments)
            .HasForeignKey(cs => cs.AircraftId);

        builder.Entity<Machinery>()
            .HasOne<Compartment>(m => m.Compartment)
            .WithMany(c => c.Machineries)
            .HasForeignKey(m => m.CompartmentId);

        builder.Entity<Weapon>()
            .HasOne<Machinery>(w => w.Machinery)
            .WithOne(m => m.Weapon)
            .HasForeignKey<Weapon>(w => w.MachineryId);
        builder.Entity<EnergySystem>()
            .HasOne<Machinery>(es => es.Machinery)
            .WithOne(m => m.EnergySystem)
            .HasForeignKey<EnergySystem>(es => es.MachineryId);
        builder.Entity<EnviromentalSystem>()
            .HasOne<Machinery>(es => es.Machinery)
            .WithOne(m => m.EnviromentalSystem)
            .HasForeignKey<EnviromentalSystem>(es => es.MachineryId);



        builder.Entity<AircraftCrew>()
            .HasKey(ac => new { ac.AircraftId, ac.MercenaryId });
        builder.Entity<MercenaryReputation>()
            .HasKey(mr => new { mr.SyndicateId, mr.MercenaryId });
        builder.Entity<EnergySystem>()
            .HasKey(es => es.MachineryId);
        builder.Entity<EnviromentalSystem>()
            .HasKey(es => es.MachineryId);
        builder.Entity<Weapon>()
            .HasKey(w => w.MachineryId);

        base.OnModelCreating(builder);
    }
}