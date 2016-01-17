using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class NestedCode
    {
        public void DiscountNested()
        {
            IEnumerable<OrderItem> itemsIn = new List<OrderItem>() {
                new OrderItem(1, "", new Product("apple", 10, "fruit"), 10),
                new OrderItem(1, "", new Product("pear", 10, "fruit"), 10),
                new OrderItem(1, "", new Product("carrot", 10, "vegetables"), 10)
            };

            IList<OrderItem> itemsOut = new List<OrderItem>();
            foreach (OrderItem item in itemsIn) {
                FilterAndDiscountItem(itemsOut, item);
            }
        }

        private void FilterAndDiscountItem(IList<OrderItem> itemsOut, OrderItem item)
        {
            Product prod = item.ItemProduct;
            if (prod.Description == "fruit")
            {
                ApplyDiscountToItem(item, 20);
                itemsOut.Add(item);
            }
        }

        private void ApplyDiscountToItem(OrderItem item, int precent)
        {
            //fortunately this does not compile, because item is immutable:
            //item.Price = item.Price * (100 - precent / 100);
        }


        public void DiscountPipelined()
        {
            IEnumerable<OrderItem> itemsIn = new List<OrderItem>() {
                new OrderItem(1, null, new Product("apple", 10, "fruit"), 10),
                new OrderItem(1, null, new Product("pear", 10, "fruit"), 10),
                new OrderItem(1, null, new Product("carrot", 10, "vegetables"), 10)
            };

            IEnumerable<OrderItem> itemsOut = itemsIn
                .Where(item => item.ItemProduct.Description == "fruit")
                .Select(item => new OrderItem(
                    item.Number, 
                    item.Description, 
                    item.ItemProduct, 
                    CalculateDiscount(item.ItemProduct.Price, 20)));
        }

        private int? CalculateDiscount(int price, int percent)
        {
            return price * (100 - percent / 100);
        }
    }
}
