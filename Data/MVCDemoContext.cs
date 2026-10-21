namespace MVCDemo.Data
{
    public class MVCDemoContext : IdentityDbContext<CustomUser>
    {
        public MVCDemoContext(DbContextOptions<MVCDemoContext> options) : base(options) { }

        public DbSet<Product> Producten { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Bestelling> Bestellingen { get; set; }
        public DbSet<Orderlijn> Orderlijnen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Klant>().ToTable("Klant");
            modelBuilder.Entity<Bestelling>().ToTable("Bestelling");
            modelBuilder.Entity<Orderlijn>().ToTable("Orderlijn");
            modelBuilder.Entity<Product>().ToTable("Product").Property(p => p.Prijs).HasColumnType("decimal(18,2)");

            // One to many 
            modelBuilder.Entity<Bestelling>()
                .HasOne(p => p.Klant)
                .WithMany(x => x.Bestellingen)
                .HasForeignKey(y => y.KlantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Many to many 
            modelBuilder.Entity<Orderlijn>()
                .HasOne(p => p.Bestelling)
                .WithMany(x => x.Orderlijnen)
                .HasForeignKey(y => y.BestellingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Orderlijn>()
                .HasOne(p => p.Product)
                .WithMany(x => x.Orderlijnen)
                .HasForeignKey(y => y.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<IdentityRole>().HasData(
                  new IdentityRole
                  {
                      Id = "b7f3c1a0-0000-0000-0000-000000000001",
                      Name = "admin",
                      NormalizedName = "ADMIN",
                      ConcurrencyStamp = "a1b2c3d4-0000-0000-0000-000000000001"
                  },
                   new IdentityRole
                   {
                       Id = "b7f3c1a0-0000-0000-0000-000000000002",
                       Name = "gebruiker",
                       NormalizedName = "GEBRUIKER",
                       ConcurrencyStamp = "a1b2c3d4-0000-0000-0000-000000000002"
                   });

            // KLANTEN
            modelBuilder.Entity<Klant>().HasData(
                new Klant {Id = 1, Naam = "Van Der Neffe", Voornaam = "Leon", AangemaaktDatum = new DateTime(2022, 10, 1) },
                new Klant {Id = 2, Naam = "Van De Kasseinen", Voornaam = "Firmin", AangemaaktDatum = new DateTime(2022, 10, 2) },
                new Klant {Id = 3, Naam = "Kiekeboe", Voornaam = "Marcel", AangemaaktDatum = new DateTime(2022, 10, 3)}
                );

            // PRODUCTEN
            modelBuilder.Entity<Product>().HasData(
                new Product {Id = 1, Naam = "fiets", Prijs = 100.00m, Beschrijving = "Dit is een fiets" },
                new Product {Id = 2, Naam = "koersfiets", Prijs = 200.00m, Beschrijving = "Dit is een mooie koersfiets" },
                new Product {Id = 3, Naam = "auto", Prijs = 2000.00m, Beschrijving = "Dit is een auto"}
                );

            // BESTELLINGEN
            modelBuilder.Entity<Bestelling>().HasData(
                new Bestelling { Id = 1, KlantId = 1 },
                new Bestelling { Id = 2, KlantId = 2 },
                new Bestelling { Id = 3, KlantId = 3 },
                new Bestelling { Id = 4, KlantId = 2 },
                new Bestelling { Id = 5, KlantId = 3 }
            );

            // ORDERLIJNEN
            modelBuilder.Entity<Orderlijn>().HasData(
                new Orderlijn { Id = 1, Aantal = 3, BestellingId = 1, ProductId = 1 },
                new Orderlijn { Id = 2, Aantal = 7, BestellingId = 1, ProductId = 2 },
                new Orderlijn { Id = 3, Aantal = 4, BestellingId = 2, ProductId = 1 },
                new Orderlijn { Id = 4, Aantal = 1, BestellingId = 2, ProductId = 2 },
                new Orderlijn { Id = 5, Aantal = 2, BestellingId = 3, ProductId = 1 },
                new Orderlijn { Id = 6, Aantal = 3, BestellingId = 4, ProductId = 1 },
                new Orderlijn { Id = 7, Aantal = 1, BestellingId = 4, ProductId = 3 },
                new Orderlijn { Id = 8, Aantal = 2, BestellingId = 5, ProductId = 1 },
                new Orderlijn { Id = 9, Aantal = 6, BestellingId = 5, ProductId = 2 },
                new Orderlijn { Id = 10, Aantal = 10, BestellingId = 5, ProductId = 3 }
            );
        }
    }
}
