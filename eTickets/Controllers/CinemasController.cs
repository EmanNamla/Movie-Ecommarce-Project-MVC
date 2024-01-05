using AutoMapper;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Cinemas;
using eTickets.PL.ViewModels.Producers;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class CinemasController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public CinemasController(IUnitofWork unitofWork,IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Cinemas = await unitofWork.Repository<Cinema>().GetAllAsync();
            var MappedCinemas = mapper.Map<IReadOnlyList<Cinema>, IReadOnlyList<CinemasViewModel>>(Cinemas);
            return View(MappedCinemas);
        }
    }
}
