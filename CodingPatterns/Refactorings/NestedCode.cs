using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorings
{
    /// <summary>
    /// Refactor Nested Code to Pipelined Code:
    /// 
    /// Before: DiscountNested() 
    /// Problems:
    /// - problem 1: model data structure is mutable
    /// - problem 2: working object is mutable and can be shared
    /// - problem 3: mutable shared object passed to other method
    /// - problem 4: filtering (read) and modification (write) is not separated
    /// - problem 5: foreign mutable object is modified in method
    /// 
    /// After: DiscountPipelined()
    /// - solution 1: model data is immutable
    /// - solution 2: there is no working data, but only final result data
    /// - solution 3: passed object is immutable
    /// - solution 4: transformation of data is separated from filtering
    /// - solution 5: passed object is not modified here
    /// </summary>
    public class NestedCode
    {
        /// <summary>
        /// 
        /// </summary>
        public void DiscountNested()
        {
            //problem 1: model data structure is mutable:
            IEnumerable<Model.Mutable.OrderItem> itemsIn = new List<Model.Mutable.OrderItem>() {
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("apple", 10, "fruit"), 10),
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("pear", 10, "fruit"), 10),
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("carrot", 10, "vegetables"), 10)
            };

            //problem 2: working object is mutable and can be shared:
            IList<Model.Mutable.OrderItem> itemsOut = new List<Model.Mutable.OrderItem>();

            foreach (Model.Mutable.OrderItem item in itemsIn) {

                //problem 3: mutable shared object passed to other method:
                FilterAndDiscountItem(itemsOut, item);
            }
        }

        private void FilterAndDiscountItem(IList<Model.Mutable.OrderItem> itemsOut, Model.Mutable.OrderItem item)
        {
            //problem 4: filtering (read) and modification (write) is not separated

            Model.Mutable.Product prod = item.ItemProduct;
            if (prod.Description == "fruit") {

                //problem 3 again: mutable item passed to other method:
                ApplyDiscountToItem(item, 20);

                //problem 5: foreign mutable itemsOut is modified here:
                itemsOut.Add(item);
            }
        }

        private void ApplyDiscountToItem(Model.Mutable.OrderItem item, int precent)
        {
            //problem 5 again: foreign mutable item is modified here:
            item.Price = item.Price * (100 - precent / 100);
        }


        public void DiscountPipelined()
        {
            //solution 1: model data is immutable:
            IEnumerable<Model.OrderItem> itemsIn = new List<Model.OrderItem>() {
                new Model.OrderItem(1, null, new Model.Product("apple", 10, "fruit"), 10),
                new Model.OrderItem(1, null, new Model.Product("pear", 10, "fruit"), 10),
                new Model.OrderItem(1, null, new Model.Product("carrot", 10, "vegetables"), 10)
            };

            //solution 2: there is no working data, but only final result data
            IEnumerable<Model.OrderItem> itemsOut = itemsIn

                //solution 3: passed item is immutable and never modified:
                .Where(item => item.ItemProduct.Description == "fruit")

                //solution 4: transformation of data is separated from filtering:
                .Select(item => new Model.OrderItem(
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
