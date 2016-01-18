using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorings
{
    /// <summary>
    /// 
    /// </summary>
    public class Example1Step2
    {
        public void DiscountNested()
        {
            //solution 1: model data is immutable:
            IEnumerable<Model.OrderItem> items1 = new List<Model.OrderItem>() {
                new Model.OrderItem(1, "", new Model.Product("apple", 10, "fruit"), 10),
                new Model.OrderItem(1, "", new Model.Product("pear", 10, "fruit"), 10),
                new Model.OrderItem(1, "", new Model.Product("carrot", 10, "vegetables"), 10)
            };

            //solution 4: transformation of data is separated from filtering:
            IList<Model.OrderItem> items2 = new List<Model.OrderItem>();   //level 0: main method
            foreach (var item in items1) {                                 //level 1: iteration block
                if (item.Description == "fruit") {                         //level 2: condition block              
                    items2.Add(item);                                      
                }
            }

            IList<Model.OrderItem> items3 = new List<Model.OrderItem>();   //level 0: main method
            foreach (var item in items2) {                                 //level 1: iteration block
                //solution 3: passed item is immutable and never modified:
                Model.OrderItem discountedItem = new Model.OrderItem(      //level 2: operation
                    item.Number,
                    item.Description,
                    item.ItemProduct,
                    CalculateDiscount(item.ItemProduct.Price, 20));
                items3.Add(discountedItem);
            }
        }

        private int CalculateDiscount(int price, int percent)
        {
            //solution 5: passed price is not modified here
            return price * (100 - percent / 100);
        }

    }
}
