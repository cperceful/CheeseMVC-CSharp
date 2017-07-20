using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddEditCheeseViewModel : AddCheeseViewModel
    {

        public int CheeseId { get; set; }

        public AddEditCheeseViewModel() : base() { }

        public AddEditCheeseViewModel(IEnumerable<CheeseCategory> categories) : base(categories) { }

        //public AddEditCheeseViewModel(int cheeseId) : base()
        //{

        //    Cheese cheese = CheeseData.GetById(cheeseId);
        //    Name = cheese.Name;
        //    Description = cheese.Description;
        //    Type = cheese.Type;
        //    CheeseId = cheese.CheeseId;

        //}

    }
}
