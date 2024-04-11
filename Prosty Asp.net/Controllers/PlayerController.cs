using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab02.Models;
namespace Lab02.Controllers {
    public class PlayerController : Controller {
        private readonly MyDbContext _Context;
        public PlayerController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index() {
            var Players = _Context.Players.ToList();
            if(Players != null) {
                return View(Players);
            }
            return View("Error");
        }
        /*public IActionResult Edit(int id) {
            var player = _Context.Players.SingleOrDefault(p => p.Id == id);
            if(player !=  null) {
                var positions = _Context.Positions.ToList();
                var positionList = new List<SelectListItem>();

                var teams = _Context.Teams.ToList();
                var TeamList = new List<SelectListItem>();

                var playerPositionList = new List<SelectListItem>();
                if(player.Positions != null) {
                    foreach(var position in player.Positions) {
                        string text = position.Name;
                        string value = position.Id.ToString();
                        playerPositionList.Add(new SelectListItem(text, value));
                    }
                }
                foreach(var position in positions) {
                    string text = position.Name;
                    string value = position.Id.ToString();
                    positionList.Add(new SelectListItem(text, value));
                }

                foreach(var team in teams) {
                    string text = team.Name + ", " + team.Country + ", " + team.City;
                    string value = team.Id.ToString();
                    TeamList.Add(new SelectListItem(text, value));
                }
                
                ViewBag.PlayerPositionList = playerPositionList;
                ViewBag.PositionList = positionList;
                ViewBag.TeamList = TeamList;

                return View(player);
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult Edit(Player player) { 
            if(player != null) {
                try {
                    _Context.Players.Update(player);
                    _Context.SaveChanges();
                } catch (Exception) {
                    return View("Error");
                }
                return View("Edited", player);
            }
            return View("Error");
        }*/
		public IActionResult Add() {
			var positions = _Context.Positions.ToList();
			var positionList = new List<SelectListItem>();

			var teams = _Context.Teams.ToList();
			var TeamList = new List<SelectListItem>();

			foreach(var position in positions) {
				string text = position.Name;
				string value = position.Id.ToString();
				positionList.Add(new SelectListItem(text, value));
			}

			foreach(var team in teams) {
				string text = team.Name + ", " + team.Country + ", " + team.City;
				string value = team.Id.ToString();
				TeamList.Add(new SelectListItem(text, value));
			}

			ViewBag.PositionList = positionList;
			ViewBag.TeamList = TeamList;

			return View();
		}
		[HttpPost]
        public IActionResult Add(Player player) {
            if(ModelState.IsValid) {
                var team = _Context.Teams.FirstOrDefault(t => t.Id == player.TeamId);
                if(team == null) {
                    ViewBag.ContextErr = "brak drużyny (null)";
                    return View("Error");
                }
                player.Team = team;


                _Context.Players.Add(player);
                try {
                    _Context.SaveChanges();
                } catch(Exception) {
                    ViewBag.ContextErr = "błąd zapisu";
                    return View("Error");
                }
                return View("Added", player);
            }
            ViewBag.ContextErr = "błąd odczytu danych";
            return View("Error");
        }
    }
}
