using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        Dictionary<string, string> Cheeses = new Dictionary<string, string>();

        // GET: /<controller>/
        public IActionResult Index()
        {
           
            ViewBag.cheeses = Cheeses; //this passses to the view, allowing it to be accessed

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string description)
        {
            Cheeses.Add(name, description);

            return Redirect("/cheese");
        }

        
    }


}
