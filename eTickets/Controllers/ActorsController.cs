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

        public ActorsController(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Actors = await unitofWork.Repository<Actor>().GetAllAsync();
            var MappedActors = mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorsViewModel>>(Actors);
            return View(MappedActors);
        }

        #region Actions Create
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorsViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedActor = mapper.Map<ActorsViewModel, Actor>(model);
                    await unitofWork.Repository<Actor>().AddAsync(MappedActor);
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


        #region Action Details
            public async Task<IActionResult> Details(int? id, string? ViewName = "Details")
        {
            if (id is null) { return BadRequest(); }
            var Actors = await unitofWork.Repository<Actor>().GetByIdAsync(id.Value);
            if (Actors is null) { return NotFound(); }
            var MappedActors = mapper.Map<Actor, ActorsViewModel>(Actors);
            return View(ViewName, MappedActors);
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
        public async Task<IActionResult> Edit([FromRoute] int? id, ActorsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedActor = mapper.Map<ActorsViewModel, Actor>(model);
                    unitofWork.Repository<Actor>().Update(MappedActor);
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

        #region Actions Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, ActorsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedActor = mapper.Map<ActorsViewModel, Actor>(model);
                    unitofWork.Repository<Actor>().Delete(MappedActor);
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
