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
        public DateTime CreationDate { get; private set; }
        public IEnumerable<OrderItem> Items { get; private set; }
        public Order(int num, string desc, DateTime date, IEnumerable<OrderItem> items)
        {
            this.Number = num;
            this.Description = desc;
            this.CreationDate = date;
            this.Items = items;
        }
    }
}
