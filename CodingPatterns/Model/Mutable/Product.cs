using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mutable
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public Product(string name, int price, string desc = null)
        {
            this.Name = name;
            this.Price = price;
            this.Description = desc;
        }
    }
}
