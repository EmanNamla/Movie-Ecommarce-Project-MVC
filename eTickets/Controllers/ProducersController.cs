﻿using AutoMapper;
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
    }
}
