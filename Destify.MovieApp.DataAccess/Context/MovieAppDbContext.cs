using Destify.MovieApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Context
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieRating> Ratings { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<MovieRating>().ToTable("MovieRating");
            modelBuilder.Entity<MovieActor>().ToTable("MovieActor");

            modelBuilder.Entity<Movie>().HasData(
               new Movie
               {
                   Id = 1,
                   Title = "The Godfather 1",
                   Description = " is a 1972 American epic crime film[2] directed by Francis Ford Coppola, who co-wrote the screenplay with Mario Puzo,"
               },
                new Movie
                {
                    Id = 2,
                    Title = "Titanic",
                    Description = "Romantic overrated movie"

                }
           );

            modelBuilder.Entity<Actor>().HasData(
              new Actor
              {
                  Id = 1,
                  Name = "Al Paccino",
                  Country = "Italy"
              },
               new Actor
               {
                   Id = 2,
                   Name = "Marlon Brando",
                   Country = "USA"
               },
               new Actor
               {
                   Id = 3,
                   Name = "Leonardo Dicaprio",
                   Country = "USA"
               },
               new Actor
               {
                   Id = 4,
                   Name = "Kate Winslet",
                   Country = "England"
               }
          );


            modelBuilder.Entity<MovieActor>().HasData(
              new MovieActor
              {
                  Id = 1,
                  MovieId = 1,
                  ActorId = 1
              },
              new MovieActor
              {
                  Id = 2,
                  MovieId = 1,
                  ActorId = 2
              },
              new MovieActor
              {
                  Id = 3,
                  MovieId = 2,
                  ActorId = 3
              },
              new MovieActor
              {
                  Id = 4,
                  MovieId = 2,
                  ActorId = 4
              });

            modelBuilder.Entity<MovieRating>().HasData(
                new MovieRating() { Id = 1, MovieId = 1, Rating = 10 },
                new MovieRating() { Id = 2, MovieId = 1, Rating = 9 },
                new MovieRating() { Id = 3, MovieId = 2, Rating = 5 },
                new MovieRating() { Id = 4, MovieId = 2, Rating = 4 });
        }
    }
}
