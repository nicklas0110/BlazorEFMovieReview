using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class RepositoryDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options, ServiceLifetime serviceLifetime) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //REVIEW MODEL BUILDER
        //Auto generate ID
        modelBuilder.Entity<Review>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        //Foregin key to Movie ID
        modelBuilder.Entity<Review>()
            .HasOne(review => review.Movie)
            .WithMany(movie => movie.Reviews)
            .HasForeignKey(review => review.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        //Type MODEL BUILDER
        //Auto generate ID
        modelBuilder.Entity<Movie>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<Movie> MovieTable { get; set; }
    public DbSet<Review> ReviewTable { get; set; }
}