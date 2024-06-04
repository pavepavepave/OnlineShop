using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopWebApp.Controllers
{
    public class TutoringSessionsController : Controller
    {
        private readonly ITutoringSessionRepository _tutoringSessionRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TutoringSessionsController(ITutoringSessionRepository tutoringSessionRepository, ITimeSlotRepository timeSlotRepository)
        {
            _tutoringSessionRepository = tutoringSessionRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create(DateTime? date, string serviceType)
        {
            if (date.HasValue && !string.IsNullOrEmpty(serviceType))
            {
                var availableTimeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync(date.Value, serviceType);
                ViewBag.TimeSlots = new SelectList(availableTimeSlots, "Id", "StartTime");
            }
            else
            {
                ViewBag.TimeSlots = new SelectList(Enumerable.Empty<TimeSlot>(), "Id", "StartTime");
            }

            ViewBag.ServiceTypes = new SelectList(new[] { "Репетиторство", "Мастер-класс по рисованию", "Мастер-класс по тафтингу", "Арт-корпоратив" });
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TutoringSession session)
        {
            var existingSession = await _tutoringSessionRepository.ExistsAsync(session.Date, session.TimeSlotId, session.ServiceType);

            if (existingSession)
            {
                ModelState.AddModelError("", "Это время уже занято.");
                var availableTimeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync(session.Date, session.ServiceType);
                ViewBag.TimeSlots = new SelectList(availableTimeSlots, "Id", "StartTime");
                ViewBag.ServiceTypes = new SelectList(new[] { "Репетиторство", "Мастер-класс по рисованию", "Мастер-класс по тафтингу", "Арт-корпоратив" });
                return View(session);
            }

            await _tutoringSessionRepository.AddAsync(session);
            return RedirectToAction(nameof(Success), new { id = session.Id });
        }

        public async Task<IActionResult> Success(Guid id)
        {
            var session = await _tutoringSessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Check(Guid bookingId)
        {
            var session = await _tutoringSessionRepository.GetByIdAsync(bookingId);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }
    }
}
