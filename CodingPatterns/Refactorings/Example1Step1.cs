using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorings
{
    public class Example1Step1
    {
        //problem 1: model data structure is mutable:
        IEnumerable<Model.Mutable.OrderItem> ItemsIn = new List<Model.Mutable.OrderItem>() {
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("apple", 10, "fruit"), 10),
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("pear", 10, "fruit"), 10),
                new Model.Mutable.OrderItem(1, "", new Model.Mutable.Product("carrot", 10, "vegetables"), 10)
            };

        //problem 2: working object is mutable and shared:
        IList<Model.Mutable.OrderItem> ItemsOut = new List<Model.Mutable.OrderItem>();

        /// <summary>
        /// 
        /// </summary>
        public void DiscountNested()
        {
            foreach (Model.Mutable.OrderItem item in ItemsIn)            //level 0: main method
            {
                //problem 3: mutable object passed to other method:
                FilterAndDiscountItem(item);                             //level 1: foreach block
            }
        }

        private void FilterAndDiscountItem(Model.Mutable.OrderItem item)
        {
            //problem 4: filtering (read) and modification (write) is not separated

            Model.Mutable.Product prod = item.ItemProduct;
            if (prod.Description == "fruit")                             //level 2: condition block
            {

                //problem 3 again: mutable item passed to other method:
                ApplyDiscountToItem(item, 20);

                //problem 5: shared mutable itemsOut is modified here:
                ItemsOut.Add(item);
            }
        }

        private void ApplyDiscountToItem(Model.Mutable.OrderItem item, int precent)
        {
            //problem 5 again: foreign mutable item is modified here:
            item.Price = item.Price * (100 - precent / 100);            //level 3: operation block
        }
    }
}
