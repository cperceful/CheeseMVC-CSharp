using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using Microsoft.EntityFrameworkCore;

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

            IList<Cheese> cheeses = context.Cheeses.Include(x => x.Category).ToList();

            return View(cheeses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = context.Categories.Single(x => x.ID == addCheeseViewModel.CategoryID);
                
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCategory,
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
            Cheese cheese = context.Cheeses.Include(x => x.Category).Single(x => x.ID == cheeseId);

            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel(context.Categories.ToList())
            {
                Name = cheese.Name,
                Description = cheese.Description,
                CategoryID = cheese.CategoryID,
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

            Cheese editCheese = context.Cheeses.Include(x => x.Category).Single(x => x.ID == addEditCheeseViewModel.CheeseId);
            CheeseCategory newCategory = context.Categories.Single(x => x.ID == addEditCheeseViewModel.CategoryID);

            if (ModelState.IsValid)
            {
                editCheese.Name = addEditCheeseViewModel.Name;
                editCheese.Description = addEditCheeseViewModel.Description;
                editCheese.Category = newCategory;
                editCheese.Rating = addEditCheeseViewModel.Rating;

                context.SaveChanges();

                return Redirect("/");
            }

            return View(addEditCheeseViewModel);

            
        }
        

        
    }


}
