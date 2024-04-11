using Microsoft.AspNetCore.Mvc;
using Lab02.Models;
namespace Lab02.Controllers {
    public class TagController : Controller {
        private readonly MyDbContext _Context;
        public TagController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index() {
            var tag = _Context.Tags.ToList();
            if (tag != null) { 
                return View(tag);
            }
            return View("Error");
        }
        public IActionResult Add() {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Tag tag) {
			if(ModelState.IsValid) {
				_Context.Tags.Add(tag);
				try {
					_Context.SaveChanges();
				} catch(Exception) {
					return View("error");
				}
				return View("Added", tag);
			}
			return View("error");
		}
    }
}
