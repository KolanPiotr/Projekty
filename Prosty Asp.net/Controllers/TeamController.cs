using Lab02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Lab02.Controllers {
    public class TeamController : Controller {
        private readonly MyDbContext _Context;

		public TeamController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index() {
            var team = _Context.Teams.ToList();
            if(team != null) {
                return View(team);
            }
            return View("Error");
        }
        public IActionResult Add() {
            var leagues = _Context.Leagues.ToList();
            var leagueList = new List<SelectListItem>();
            
            foreach(var league in leagues) {
                string text = league.Level + ", " + league.Country + ", " + league.Name;
                string value = league.Id.ToString();
                leagueList.Add(new SelectListItem (text, value));
            }
            
            ViewBag.leagueList = leagueList;
            
            return View();
        }
        [HttpPost]
		public IActionResult Add(Team team) {
			if(ModelState.IsValid) {
				var league = _Context.Leagues.FirstOrDefault(l => l.Id == team.LeagueId);
				if(league == null) {
					ViewBag.ContextErr = "League is null";
					return View("Error");
				}
				team.League = league;

				_Context.Teams.Add(team);
				try {
					_Context.SaveChanges();
				}
				catch(Exception) {
					ViewBag.ContextErr = "Cannot save changes";
					return View("Error");
				}
				return View("Added", team);
			}
			ViewBag.ContextErr = "Model state is invalid";
			return View("Error");
		}
		/*public IActionResult Add(Team team) { 
            if(ModelState.IsValid) {
                
                var league = _Context.Leagues.FirstOrDefault(league => league.Id == team.LeagueId);
                if(league == null) {
					ViewBag.ContextErr = "brak drużyny (null)";
					return View("Error");
                }
				team.League = league;
				

				_Context.Teams.Add(team);
				try {
					int savedChanges = _Context.SaveChanges();
					if(savedChanges > 0) {
						return View("Added", team);
					}
					else {
						ViewBag.ContextErr = "No changes were saved to the database.";
						return View("Error");
					}
				} catch  (Exception) {
                    ViewBag.ContextErr = "nie można zapisać drużyny w bazie"; 
                    return View("Error");
                }
            }
			ViewBag.ContextErr = "team is not valid";
			return View("Error");
        }*/
    }
}
