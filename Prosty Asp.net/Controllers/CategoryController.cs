using Lab02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers {
    public class CategoryController : Controller {
    
        private readonly MyDbContext _Context;
        public CategoryController(MyDbContext context) {
            _Context = context;
        }
        public IActionResult Index() {
            var categories = _Context.Categories.ToList();
            if(categories != null) {
                return View(categories);
            }
            return View("Error");
        }
        public IActionResult Add() { 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category) {
            if(ModelState.IsValid) {
                _Context.Categories.Add(category);
                try {
                    _Context.SaveChanges();
                }
                catch(Exception ex) {
                    return View("error");
                }
                return RedirectToAction("Index");
            }
            return View("error");
        }
    }
}
