using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderItem
    {
        public int Number { get; private set; }
        public string Description { get; private set;}
        public int? Price { get; private set; }
        public Product ItemProduct { get; private set; }

        public OrderItem(int num, string desc, Product prod = null, int? price = null)
        {
            this.Number = num;
            this.Description = desc;
            this.ItemProduct = prod;
            this.Price = price;
        }
    }
}
