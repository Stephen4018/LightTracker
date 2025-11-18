using LightUsageTracker_Core.Core.Entities;
using LightUsageTracker_Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightUsageTracker.Controllers
{
    public class Computation : Controller
    {
        private readonly ITrackerLogic _trackerLogic;
        private readonly IComputation _computation;

        public Computation(ITrackerLogic trackerLogic, IComputation computation)
        {
            _trackerLogic = trackerLogic;
            _computation = computation;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _trackerLogic.GetUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<Guid, decimal> newUsages, decimal kilowattRate)
        {
            var trackerId = await _computation.CalculateLightUsage(newUsages, kilowattRate);
            return RedirectToAction("Results", new { trackerId });
        }


        [HttpGet]
        public async Task<IActionResult> Results(string trackerId)
        {
            var results = await _computation.GetLightUsages(trackerId);

            //ViewBag.Users = user;

            ViewBag.BatchId = trackerId; 
            return View(results);
        }
    }
}
