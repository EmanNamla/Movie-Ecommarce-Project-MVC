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
            CreateMap<Movie, MovieViewModel>()
            .ForMember(m => m.CinemaName, o => o.MapFrom(m => m.Cinema.Name))
            .ForMember(m => m.ProducerName, o => o.MapFrom(m => m.Producer.FullName))
            .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(src => src.Actors_Movies.Select(am => am.ActorId).ToList()))
            .ReverseMap();


        }
    }
}
