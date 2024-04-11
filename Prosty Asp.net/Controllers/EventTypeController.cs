using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers {
    public class EventTypeController : Controller {
        private readonly MyDbContext _Context;
        public EventTypeController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index() {
            var eventType = _Context.eventTypes.ToList();
            if (eventType != null) {
                return View(eventType);
            }
            return View("Error");
        }
        public IActionResult Add() {
            return View();
        }
        [HttpPost]
        public IActionResult Add(EventType eventType) {
            if(ModelState.IsValid) {
                _Context.eventTypes.Add(eventType);
                try {
                    _Context.SaveChanges();
                }
                catch(Exception) {
                    return View("error");
                }
                return View("Added", eventType);
            }
            return View("error");
        }
    }
}
