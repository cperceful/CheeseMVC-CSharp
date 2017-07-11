using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseData
    {

        static private List<Cheese> cheeses = new List<Cheese>();

        //GetAll
        public static List<Cheese> GetAll()
        {
            return cheeses;
        }

        //AddCheese

        public static void Add(Cheese cheese)
        {
            cheeses.Add(cheese);
        }

        //RemoveCheese

        public static void Remove(int cheeseId)
        {
            cheeses.Remove(GetById(cheeseId));
        }

        //GetById

        public static Cheese GetById(int id)
        {
            return cheeses.Single(x => x.CheeseId == id);
        }

    }
}
