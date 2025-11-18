using LightUsageTracker_Core.Core.Entities;
using LightUsageTracker_Core.Services.DTOs;
using LightUsageTracker_Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightUsageTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly ITrackerLogic _trackerLogic;
        public UserController(ITrackerLogic trackerLogic)
        {
            _trackerLogic = trackerLogic;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _trackerLogic.GetUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            if (ModelState.IsValid)
            {
                var created = await _trackerLogic.CreateUser(user);
                if(created)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to add user. Please try again.");
                    return View(user);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _trackerLogic.GetUserById(id);
            if (user == null) return NotFound("User was not found");
            var hasBeenComputed = await _trackerLogic.CheckIfUserIsComputed(id);

            ViewBag.HasBeenComputed = hasBeenComputed;
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Users user)
        {
           var UpdatedUser = await  _trackerLogic.EditUser(user);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to Update User. Please try again.");
                return View(user);

            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetLightUsagesForSingleUser(Guid id)
        {
            var user = await _trackerLogic.GetUserById(id);
            if (user == null) return NotFound("User was not found");

            var usage = await _trackerLogic.GetLightUsagesForSingleUser(id);
            if (usage == null) return NotFound("Usage was not found");
            //var hasBeenComputed = await _trackerLogic.CheckIfUserIsComputed(id);

            ViewBag.Users = user;

            return View(usage);
        }


    }
}
