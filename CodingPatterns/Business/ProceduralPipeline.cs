using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProceduralPipeline
    {
        void CallStages()
        {
            IEnumerable<OrderItem> orderItems = CreateItems();
            IEnumerable<OrderItem> cheapItems = FilterItems(orderItems, item => item.Price < 100);
            IEnumerable<string> priceList = GetPriceList(orderItems);
        }

        IEnumerable<string> ProcessStages(IEnumerable<OrderItem> itemList)
        {
            IEnumerable<OrderItem> cheapItems = FilterItems(itemList, item => item.Price < 100);
            IEnumerable<string> priceList = GetPriceList(itemList);
            return priceList;
        }

        public static IEnumerable<OrderItem> CreateItems()
        {
            IEnumerable<OrderItem> itemList = new List<OrderItem>()
            {
                new OrderItem(1, "apple", price: 10),
                new OrderItem(2, "banana", price: 20),
                new OrderItem(3, "pea", price: 15),
                new OrderItem(4, "caviar", price: 115),
                new OrderItem(5, "lobster", price: 105)
            };
            return itemList;
        }

        public static IEnumerable<OrderItem> FilterItems(IEnumerable<OrderItem> orders, Func<OrderItem, bool> predicate)
        {
            return orders.Where(predicate);
        }

        private IEnumerable<string> GetPriceList(IEnumerable<OrderItem> orderItems)
        {
            return orderItems
                .Select(item => item.Number + ". " + item.Description + ": " + item.Price);
        }

    }
}
