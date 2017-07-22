using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        [Required]
        public int CheeseId { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel() { }

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Menu = menu;
            Cheeses = new List<SelectListItem>();

            foreach(Cheese cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
        }
    }
}
