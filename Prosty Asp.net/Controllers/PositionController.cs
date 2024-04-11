using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers {
	public class PositionController : Controller {
		private readonly MyDbContext _Context;
		public PositionController(MyDbContext context) {
			_Context = context;
		}

		public IActionResult Index() {
			var Position = _Context.Positions.ToList();
			if(Position != null) {
				return View(Position);
			}
			return View("Error");
		}
		public IActionResult Add() {
			return View();
		}

		[HttpPost]
		public IActionResult Add(Position position) {
			if(ModelState.IsValid) {
				_Context.Positions.Add(position);
				try {
					_Context.SaveChanges();
				}
				catch(Exception) {
					return View("error");
				}
				return View("Added", position);
			}
			return View("error");

        }
    }
}
