using AutoMapper;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Actors;

namespace eTickets.PL.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Actor,ActorsViewModel>().ReverseMap();
        }
    }
}
