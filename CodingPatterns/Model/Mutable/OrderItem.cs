using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mutable
{
    public class OrderItem
    {
        public int Number { get; set; }
        public string Description { get; set;}
        public int? Price { get; set; }
        public Product ItemProduct { get; set; }

        public OrderItem(int num, string desc, Product prod = null, int? price = null)
        {
            this.Number = num;
            this.Description = desc;
            this.ItemProduct = prod;
            this.Price = price;
        }
    }
}
