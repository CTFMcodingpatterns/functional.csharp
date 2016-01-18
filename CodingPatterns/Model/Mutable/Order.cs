using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mutable
{
    public class Order
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public Order(int num, string desc, DateTime date, IEnumerable<OrderItem> items)
        {
            this.Number = num;
            this.Description = desc;
            this.CreationDate = date;
            this.Items = items;
        }
    }
}
