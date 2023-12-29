using AutoMapper;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Actors;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public ActorsController(IUnitofWork unitofWork,IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Actors=await unitofWork.Repository<Actor>().GetAllAsync();
            var MappedActors = mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorsViewModel>> (Actors);
            return View(MappedActors);
        }
    }
}
