using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorings
{
    public class Example1Step3
    {
        public void DiscountPipelined()
        {
            //solution 1: model data is immutable:
            IEnumerable<Model.OrderItem> itemsIn = new List<Model.OrderItem>() {
                new Model.OrderItem(1, null, new Model.Product("apple", 10, "fruit"), 10),
                new Model.OrderItem(1, null, new Model.Product("pear", 10, "fruit"), 10),
                new Model.OrderItem(1, null, new Model.Product("carrot", 10, "vegetables"), 10)
            };

            //solution 2: there is no working data, but only final result data
            IEnumerable<Model.OrderItem> itemsOut = itemsIn                 //level 0: main method

                //solution 3: passed item is immutable and never modified:
                .Where(item => item.ItemProduct.Description == "fruit")     //level 1/2: where and lambda filter

                //solution 4: transformation of data is separated from filtering:
                .Select(item => new Model.OrderItem(                        //level 1/2: select and lambda operation
                    item.Number,
                    item.Description,
                    item.ItemProduct,
                    CalculateDiscount(item.ItemProduct.Price, 20)));
        }

        private int? CalculateDiscount(int price, int percent)
        {
            //solution 5: passed price is not modified here:
            return price * (100 - percent / 100);
        }
    }
}
