using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers {
    public class AuthorController : Controller {
        private readonly MyDbContext _Context;
        public AuthorController(MyDbContext context) {
            _Context = context;
        }

        public IActionResult Index() {
            var Authors = _Context.Authors.ToList();
            if (Authors != null) {
                return View(Authors);
            }
            return View("Error");
        }
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author author) {
            if(ModelState.IsValid) {
                _Context.Authors.Add(author); 
                try {
                    _Context.SaveChanges();
                } catch (Exception ex) {
                    return View("error");
                }
				return View("Added", author);
			}
            return View("error");
            
        }
    }
}
