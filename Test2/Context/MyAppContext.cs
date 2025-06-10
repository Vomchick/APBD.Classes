using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Context;

public class MyAppContext : DbContext
{
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }
    public virtual DbSet<BookAuthor> BookAuthors { get; set; }
    public virtual DbSet<BookGenre> BookGenres { get; set; }

    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(x => x.Authors)
            .WithMany(x => x.Books)
            .UsingEntity<BookAuthor>();

        modelBuilder.Entity<Genre>()
            .HasMany(x => x.Books)
            .WithMany(x => x.Genres)
            .UsingEntity<BookGenre>();

        modelBuilder.Entity<PublishingHouse>()
            .HasMany(x => x.Books)
            .WithOne(x => x.PublishingHouse)
            .HasForeignKey(x => x.PublishingHouseId);

        modelBuilder.AddInitialData();
    }
}
