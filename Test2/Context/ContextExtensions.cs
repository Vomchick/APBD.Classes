using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Test2.Models;

namespace Test2.Context;

internal static class ContextExtensions
{
    public static void AddInitialData(this ModelBuilder modelBuilder)
    {
        var publiser = new PublishingHouse
        {
            Id = 1,
            Name = "Penguin Random House",
            City = "New York",
            Country = "USA"
        }; 
        modelBuilder.Entity<PublishingHouse>().HasData(
            publiser
        );


        var author = new Author
        {
            Id = 1,
            FirstName = "George",
            LastName = "Orwell",
        };

        modelBuilder.Entity<Author>().HasData(
            author
        );

        var genre = new Genre
        {
            Id = 1,
            Name = "Dystopian"
        };
        modelBuilder.Entity<Genre>().HasData(
            genre
        );
    }
 }
