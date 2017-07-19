using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext; //this creates the controller receiving an instance of CheeseDbContext
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Cheese> cheeses = context.Cheeses.ToList();

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
                    Type = addCheeseViewModel.Type,
                    Rating = addCheeseViewModel.Rating
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/cheese");
            }

            return View(addCheeseViewModel);
        }

        [HttpGet]
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(cheeses);
        }

        
        [HttpPost]
        public IActionResult Remove(int cheeseId)
        {
            Cheese cheese = context.Cheeses.Single(x => x.ID == cheeseId);
            context.Cheeses.Remove(cheese);
            context.SaveChanges();

            return Redirect("/");

        }
        
        [HttpGet]
        [Route("/cheese/edit/{cheeseId}")] 
        public IActionResult Edit(int cheeseId)
        {
            Cheese cheese = context.Cheeses.Single(x => x.ID == cheeseId);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel
            {
                Name = cheese.Name,
                Description = cheese.Description,
                Type = cheese.Type,
                Rating = cheese.Rating,
                CheeseId = cheese.ID
            };          

            
            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        [Route("cheese/edit/{cheeseId}")]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {

            //Cheese editCheese = CheeseData.GetById(addEditCheeseViewModel.CheeseId);

            Cheese editCheese = context.Cheeses.Single(x => x.ID == addEditCheeseViewModel.CheeseId);

            if (ModelState.IsValid)
            {
                editCheese.Name = addEditCheeseViewModel.Name;
                editCheese.Description = addEditCheeseViewModel.Description;
                editCheese.Type = addEditCheeseViewModel.Type;
                editCheese.Rating = addEditCheeseViewModel.Rating;

                context.SaveChanges();

                return Redirect("/");
            }

            return View(addEditCheeseViewModel);

            
        }
        

        
    }


}
