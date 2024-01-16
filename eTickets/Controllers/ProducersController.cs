using AutoMapper;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.PL.ViewModels.Actors;
using eTickets.PL.ViewModels.Producers;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class ProducersController : Controller
    {

        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;

        public ProducersController(IUnitofWork unitofWork,IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Producers = await unitofWork.Repository<Producer>().GetAllAsync();
            var MappedProducers = mapper.Map<IReadOnlyList<Producer>, IReadOnlyList<ProducersViewModel>>(Producers);
            return View(MappedProducers);
        }

        #region Actions Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProducersViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedProducer = mapper.Map<ProducersViewModel, Producer>(model);
                    await unitofWork.Repository<Producer>().AddAsync(MappedProducer);
                    await unitofWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
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
            var Producers = await unitofWork.Repository<Producer>().GetByIdAsync(id.Value);
            if (Producers is null) { return NotFound(); }
            var MappedProducers = mapper.Map<Producer, ProducersViewModel>(Producers);
            return View(ViewName, MappedProducers);
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
        public async Task<IActionResult> Edit([FromRoute] int? id, ProducersViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedProducer = mapper.Map<ProducersViewModel, Producer>(model);
                    unitofWork.Repository<Producer>().Update(MappedProducer);
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
        public async Task<IActionResult> Delete([FromRoute] int? id, ProducersViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ProducerActor = mapper.Map<ProducersViewModel, Producer>(model);
                    unitofWork.Repository<Producer>().Delete(ProducerActor);
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
