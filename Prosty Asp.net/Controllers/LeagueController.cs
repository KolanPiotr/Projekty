using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers {
    public class LeagueController : Controller {
        private readonly MyDbContext _Context;
        public LeagueController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index(int id) {
            var league = _Context.Leagues.ToList();
            if (league != null) { 
                return View(league);
            }
            return View("Error");
        }
        public IActionResult Add() { 
            return View();
        }
        [HttpPost]
        public IActionResult Add(League league) {
            if(ModelState.IsValid) {
                _Context.Leagues.Add(league);
                try {
                    _Context.SaveChanges();
                }
                catch(Exception ex) {
                    return View("error");
                }
                return View("Added", league);
            }
            return View("error");
        }
    }
}
