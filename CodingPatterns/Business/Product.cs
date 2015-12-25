using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Product
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        public Product(string name, int price, string desc = null)
        {
            this.Name = name;
            this.Price = price;
            this.Description = desc;
        }
    }
}
