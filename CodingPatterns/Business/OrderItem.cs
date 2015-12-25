using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class OrderItem
    {
        public int Number { get; private set; }
        public string Description { get; private set;}
        public DateTime CreationDate { get; private set; }
        public Product ItemProduct { get; private set; }

        public OrderItem(int num, string desc, DateTime? date = null, Product prod = null)
        {
            this.Number = num;
            this.Description = desc;
            this.CreationDate = date ?? DateTime.Today;
            this.ItemProduct = prod;
        }
    }
}
