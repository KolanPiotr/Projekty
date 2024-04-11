using Lab02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab02.Controllers
{
    public class ArticleController : Controller
    {
        private readonly MyDbContext _dbContext;

        public ArticleController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

		public IActionResult Index() {
			var Article = _dbContext.Articles.ToList();
			if(Article != null) {
				return View(Article);
			}
			return View("Error");
		}

		public IActionResult Add()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Add(Article article) {
			if(ModelState.IsValid) {
				_dbContext.Articles.Add(article);
				try {
					_dbContext.SaveChanges();
				}
				catch(Exception) {
					return View("error");
				}
				return View("Added", article);
			}
			return View("error");
		}
    }
}
