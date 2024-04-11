using Microsoft.AspNetCore.Mvc;


namespace Lab02.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _dbContext;

        public HomeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            var articles = _dbContext.Articles.ToList(); //var articles = Repository.Articles;
            return View(articles);
        }
    }
}
