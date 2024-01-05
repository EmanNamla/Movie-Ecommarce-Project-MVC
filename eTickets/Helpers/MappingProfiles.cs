using AutoMapper;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Actors;
using eTickets.PL.ViewModels.Cinemas;
using eTickets.PL.ViewModels.Movies;
using eTickets.PL.ViewModels.Producers;

namespace eTickets.PL.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Actor,ActorsViewModel>().ReverseMap();
            CreateMap<Producer, ProducersViewModel>().ReverseMap();
            CreateMap<Cinema, CinemasViewModel>().ReverseMap();
            CreateMap<Movie, MovieViewModel>().ReverseMap();
        }
    }
}
