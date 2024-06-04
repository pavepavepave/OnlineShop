using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TutoringSessionsController : Controller
    {
        private readonly ITutoringSessionRepository _tutoringSessionRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TutoringSessionsController(ITutoringSessionRepository tutoringSessionRepository, ITimeSlotRepository timeSlotRepository)
        {
            _tutoringSessionRepository = tutoringSessionRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _tutoringSessionRepository.GetAllAsync();
            return View(sessions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var session = await _tutoringSessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            var availableTimeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync(session.Date, session.ServiceType);
            ViewBag.TimeSlots = new SelectList(availableTimeSlots, "Id", "StartTime", session.TimeSlotId);
            ViewBag.ServiceTypes = new SelectList(new[] { "Репетиторство", "Мастер-класс по рисованию", "Мастер-класс по тафтингу", "Арт-корпоратив" }, session.ServiceType);
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutoringSession session)
        {
            if (!ModelState.IsValid)
            {
                var availableTimeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync(session.Date, session.ServiceType);
                ViewBag.TimeSlots = new SelectList(availableTimeSlots, "Id", "StartTime", session.TimeSlotId);
                ViewBag.ServiceTypes = new SelectList(new[] { "Репетиторство", "Мастер-класс по рисованию", "Мастер-класс по тафтингу", "Арт-корпоратив" }, session.ServiceType);
                return View(session);
            }

            await _tutoringSessionRepository.UpdateAsync(session);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _tutoringSessionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
