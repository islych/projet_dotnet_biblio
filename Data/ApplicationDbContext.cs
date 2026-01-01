using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Data;

/// <summary>
/// Contexte Entity Framework Core pour la base de données
/// Configure les relations et l'héritage TPH pour Compte
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Compte> Comptes { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Livre> Livres { get; set; }
    public DbSet<Categorie> Categories { get; set; }
    public DbSet<Favori> Favoris { get; set; }
    public DbSet<Telechargement> Telechargements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de l'héritage TPH pour Compte
        modelBuilder.Entity<Compte>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Utilisateur>("Utilisateur")
            .HasValue<Admin>("Admin");

        // Configuration des relations

        // Relation Livre -> Categorie (Many-to-One)
        modelBuilder.Entity<Livre>()
            .HasOne(l => l.Categorie)
            .WithMany(c => c.Livres)
            .HasForeignKey(l => l.CategorieId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Favori -> Utilisateur (Many-to-One)
        modelBuilder.Entity<Favori>()
            .HasOne(f => f.Utilisateur)
            .WithMany(u => u.Favoris)
            .HasForeignKey(f => f.UtilisateurId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Favori -> Livre (Many-to-One)
        modelBuilder.Entity<Favori>()
            .HasOne(f => f.Livre)
            .WithMany(l => l.Favoris)
            .HasForeignKey(f => f.LivreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Telechargement -> Utilisateur (Many-to-One)
        modelBuilder.Entity<Telechargement>()
            .HasOne(t => t.Utilisateur)
            .WithMany(u => u.Telechargements)
            .HasForeignKey(t => t.UtilisateurId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Telechargement -> Livre (Many-to-One)
        modelBuilder.Entity<Telechargement>()
            .HasOne(t => t.Livre)
            .WithMany(l => l.Telechargements)
            .HasForeignKey(t => t.LivreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Contrainte unique : un utilisateur ne peut pas avoir le même livre en favori deux fois
        modelBuilder.Entity<Favori>()
            .HasIndex(f => new { f.UtilisateurId, f.LivreId })
            .IsUnique();

        // Configuration des index pour améliorer les performances
        modelBuilder.Entity<Livre>()
            .HasIndex(l => l.Titre);

        modelBuilder.Entity<Compte>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}

