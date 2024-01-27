using AutoMapper;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Actors;
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

        #region Actions Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemasViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedCinema = mapper.Map<CinemasViewModel, Cinema>(model);
                    await unitofWork.Repository<Cinema>().AddAsync(MappedCinema);
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
            var Cinemas = await unitofWork.Repository<Cinema>().GetByIdAsync(id.Value);
            if (Cinemas is null) { return NotFound(); }
            var MappedCinemas = mapper.Map<Cinema, CinemasViewModel>(Cinemas);
            return View(ViewName, MappedCinemas);
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
        public async Task<IActionResult> Edit([FromRoute] int? id, CinemasViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedCinema = mapper.Map<CinemasViewModel, Cinema>(model);
                    unitofWork.Repository<Cinema>().Update(MappedCinema);
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
        public async Task<IActionResult> Delete([FromRoute] int? id, CinemasViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedCinema = mapper.Map<CinemasViewModel, Cinema>(model);
                    unitofWork.Repository<Cinema>().Delete(MappedCinema);
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
