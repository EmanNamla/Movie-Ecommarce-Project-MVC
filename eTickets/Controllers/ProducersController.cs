using eTickets.BL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public ProducersController(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
