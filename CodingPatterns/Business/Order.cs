using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Order
    {
        public int Number { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<OrderItem> Items { get; private set; }
        public Order(int num, string desc, IEnumerable<OrderItem> items)
        {
            this.Number = num;
            this.Description = desc;
            this.Items = items;
        }
    }
}
