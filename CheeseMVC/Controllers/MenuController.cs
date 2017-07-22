using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Menu> menus = context.Menus.ToList();

            return View(menus);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect($"/menu/viewmenu/{newMenu.ID}");

            }

            return View(addMenuViewModel);
        }

        [HttpGet]
        [Route("/menu/viewmenu/{id}")]
        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);

            IList<CheeseMenu> items = context.CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewMenuViewModel);
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, context.Cheeses.ToList());

            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addMenuItemViewModel);
            }

            IList<CheeseMenu> existingItems = context.CheeseMenus
                .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseId)
                .Where(cm => cm.MenuID == addMenuItemViewModel.MenuId)
                .ToList();

            if (existingItems.Count == 0)
            {
                CheeseMenu newCheeseMenu = new CheeseMenu
                {
                    CheeseID = addMenuItemViewModel.CheeseId,
                    MenuID = addMenuItemViewModel.MenuId
                };

                context.CheeseMenus.Add(newCheeseMenu);
                context.SaveChanges();
                return Redirect($"/menu/viewmenu/{addMenuItemViewModel.MenuId}");
            }

            return Redirect($"/menu/viewmenu/{addMenuItemViewModel.MenuId}");
        }
    }
}
