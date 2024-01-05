using AutoMapper;
using eTickets.BL.Specifications.MovieSpecifications;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Cinemas;
using eTickets.PL.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public MovieController(IUnitofWork unitofWork,IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var spec = new MovieAndCinemaSpecification();
            var Movies =await unitofWork.Repository<Movie>().GetAllWithSpecAsync(spec);
            var MappedMovies = mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(Movies);
            return View(MappedMovies);
        }
    }
}
