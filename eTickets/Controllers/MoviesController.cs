using AutoMapper;
using eTickets.BL.Specifications.Actors_MoviesSpecifications;
using eTickets.BL.Specifications.ActorSpecifications;
using eTickets.BL.Specifications.MovieSpecifications;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Cinemas;
using eTickets.PL.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.PL.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public MoviesController(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var spec = new MovieAndCinemaSpecification();
                var Movies = await unitofWork.Repository<Movie>().GetAllWithSpecAsync(spec);
                var MappedMovies = mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(Movies);
                return View(MappedMovies);
            }
            else
            {
                var spec = new MovieFilterByName(SearchValue);
                var MovieFilterByName = await unitofWork.Repository<Movie>().GetAllWithSpecAsync(spec);
                var MappedMovies = mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(MovieFilterByName);
                return View(MappedMovies);
            }
        }

        #region Action Details
        public async Task<IActionResult> Details(int? id,string? ViewName="Details")
        {
            ViewBag.Cinemas = await unitofWork.Repository<Cinema>().GetAllAsync();
            ViewBag.Actors = await unitofWork.Repository<Actor>().GetAllAsync();
            ViewBag.Producers = await unitofWork.Repository<Producer>().GetAllAsync();
            if (id == 0) { return BadRequest(); }
            try
            {
                var movie = await unitofWork.Repository<Movie>().GetByIdAsync(id.Value);

                if (movie == null) { return NotFound(); }
                var MappedMovie = mapper.Map<Movie, MovieViewModel>(movie);

                return View(ViewName, MappedMovie);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred in Details action: {ex}");
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Actions Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Cinemas = await unitofWork.Repository<Cinema>().GetAllAsync();
            ViewBag.Actors = await unitofWork.Repository<Actor>().GetAllAsync();
            ViewBag.Producers = await unitofWork.Repository<Producer>().GetAllAsync();
            return View();

        }



        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newMovie = new Movie()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        ImageURL = model.ImageURL,
                        CinemaId = model.CinemaId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        MovieCategory = model.MovieCategory,
                        ProducerId = model.ProducerId
                    };
                    await unitofWork.Repository<Movie>().AddAsync(newMovie);
                    await unitofWork.CompleteAsync();

                    //Add Movie Actors
                    foreach (var actorId in model.ActorIds)
                    {
                        var newActorMovie = new Actors_Movies()
                        {
                            MovieId = newMovie.Id,
                            ActorId = actorId
                        };
                        await unitofWork.Repository<Actors_Movies>().AddAsync(newActorMovie);
                    }
                    await unitofWork.CompleteAsync();


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }

            return View(model);
        }
        #endregion

        #region Actions Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var OldMovie = await unitofWork.Repository<Movie>().GetByIdAsync(id.Value);
                    if (OldMovie != null)
                    {
                        OldMovie.Name = model.Name;
                        OldMovie.Price = model.Price;
                        OldMovie.Description = model.Description;
                        OldMovie.StartDate = model.StartDate;
                        OldMovie.EndDate = model.EndDate;
                        OldMovie.CinemaId = model.CinemaId;
                        OldMovie.ProducerId = model.ProducerId;
                        OldMovie.ImageURL = model.ImageURL;
                        OldMovie.MovieCategory = model.MovieCategory;
                    }
                    await unitofWork.CompleteAsync();

                    //RemoveExistingActors
                    var spec = new ActorsMoviesSpecifications(model.Id);
                    var ExistingActors = await unitofWork.Repository<Actors_Movies>().GetAllWithSpecAsync(spec);
                    unitofWork.Repository<Actors_Movies>().RemoveRenge(ExistingActors);
                    //create anewActors
                    foreach (var actorId in model.ActorIds)
                    {
                        var newActorMovie = new Actors_Movies()
                        {
                            MovieId = model.Id,
                            ActorId = actorId
                        };
                        await unitofWork.Repository<Actors_Movies>().AddAsync(newActorMovie);
                    }
                    await unitofWork.CompleteAsync();


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }
            return View(model);
        } 
        #endregion
    }
}
