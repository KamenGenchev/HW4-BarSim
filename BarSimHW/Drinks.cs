using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarSimHW
{
    class Drinks
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public Drinks (string name, float price, int quantiy)
        {
            Name = name;
            Price = price;
            Quantity = quantiy;             
        }
    }
}
