using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
       

        // GET: /<controller>/
        public IActionResult Index()
        {
         
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Type = addCheeseViewModel.Type
                };

                CheeseData.Add(newCheese);

                return Redirect("/cheese");
            }

            return View(addCheeseViewModel);
        }

        [HttpGet]
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            List<Cheese> cheeses = CheeseData.GetAll();
            return View(cheeses);
        }

        
        [HttpPost]
        public IActionResult Remove(int cheeseId)
        {
            CheeseData.Remove(cheeseId);

            

            return Redirect("/");

        }
        
        [HttpGet]
        [Route("/cheese/edit/{cheeseId}")] 
        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        [Route("cheese/edit/{cheeseId}")]
        public IActionResult Edit(string name, string description, int cheeseId)
        {

            Cheese editCheese = CheeseData.GetById(cheeseId);

            editCheese.Name = name;
            editCheese.Description = description;

            return Redirect("/");
        }
        

        
    }


}
