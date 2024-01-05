using eTickets.DAL.Contexts;
using eTickets.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eTickets.DAL.Data.DataSeed
{
    public  class AppContextSeed
    {
        public static async Task Seed(AppDbContext DbContext )
        {
            if(!DbContext.Cinemas.Any())
            {
                try
                {
                    var cinemaData = File.ReadAllText("../eTickets.DAL/Data/DataSeed/Cinemas.json");
                    var cinemas = JsonSerializer.Deserialize<List<Cinema>>(cinemaData);
                    if (cinemas?.Count > 0)
                    {
                        foreach (var cineam in cinemas)
                        {
                            await DbContext.Set<Cinema>().AddAsync(cineam);
                        }
                        await DbContext.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                }
            }

            if (!DbContext.Actors.Any())
            {
                try
                {
                    var ActorData = File.ReadAllText("../eTickets.DAL/Data/DataSeed/Actor.json");
                    var Actors = JsonSerializer.Deserialize<List<Actor>>(ActorData);
                    if (Actors?.Count > 0)
                    {
                        foreach (var actor in Actors)
                        {
                            await DbContext.Set<Actor>().AddAsync(actor);
                        }
                        await DbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                }
            }

            if (!DbContext.Producers.Any())
            {
                try
                {
                    var ProducerData = File.ReadAllText("../eTickets.DAL/Data/DataSeed/Producer.json");
                    var Producers = JsonSerializer.Deserialize<List<Producer>>(ProducerData);
                    if (Producers?.Count > 0)
                    {
                        foreach (var Producer in Producers)
                        {
                            await DbContext.Set<Producer>().AddAsync(Producer);
                        }
                        await DbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                }
            }

            if (!DbContext.Movies.Any())
            {
                try
                {
                    var MoviesData = File.ReadAllText("../eTickets.DAL/Data/DataSeed/Movie.json");
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Converters =
                        {
                            new MovieCategoryConverter()
                        }
                    };
                    var Moviess = JsonSerializer.Deserialize<List<Movie>>(MoviesData, options);
                    if (Moviess?.Count > 0)
                    {
                        foreach (var Movie in Moviess)
                        {
                            await DbContext.Set<Movie>().AddAsync(Movie);
                        }
                        await DbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                }
            }

            if (!DbContext.Actors_Movies.Any())
            {
                try
                {
                    var Actors_MoviesData = File.ReadAllText("../eTickets.DAL/Data/DataSeed/Actor_Movie.json");
                    var Actors_Movies = JsonSerializer.Deserialize<List<Actors_Movies>>(Actors_MoviesData);
                    if (Actors_Movies?.Count > 0)
                    {
                        foreach (var Actors_Movie in Actors_Movies)
                        {
                            await DbContext.Set<Actors_Movies>().AddAsync(Actors_Movie);
                        }
                        await DbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                }
            }
        }
    }
}
