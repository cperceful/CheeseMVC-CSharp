using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
       

        // GET: /<controller>/
        public IActionResult Index()
        {
           
            ViewBag.cheeses = CheeseData.GetAll(); //this passses to the view, allowing it to be accessed

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Cheese newCheese)
        {
            
            CheeseData.Add(newCheese);
            
            return Redirect("/cheese");
        }

        [HttpGet]
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        
        [HttpPost]
        public IActionResult Remove(int cheeseId)
        {
            CheeseData.Remove(cheeseId);

            

            return Redirect("/");

        }

        [HttpGet]
        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string name, string description, int cheeseId)
        {

            Cheese editCheese = CheeseData.GetById(cheeseId);

            editCheese.Name = name;
            editCheese.Description = description;

            return View();
        }
        

        
    }


}
