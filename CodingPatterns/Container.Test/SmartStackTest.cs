using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using log4net;

namespace Container.Test
{
    [TestFixture]
    class SmartStackTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SmartStackTest));

        [Test]
        public void Of_ImmuStack_ReturnsSmartStack()
        {
            //Arrange
            ImmuStack<string> stack = ImmuStack<string>.Empty()
                .Push("a")
                .Push("b");

            //Act
            SmartStack<string> smartStack = SmartStack<string>.Of(stack);

            //Assert
            //todo
            Logger.Debug("smartStack: " + smartStack);
            Assert.IsInstanceOf<SmartStack<string>>(smartStack);
        }

        [Test]
        public void Of_List_ReturnsSmartStack()
        {
            //Arrange
            IEnumerable<string> list = new List<string>() { "a", "b", "c" };

            //Act
            SmartStack<string> smartStack = SmartStack<string>.Of(list);

            //Assert
            //todo
            Logger.Debug("smartStack: " + smartStack);
            Assert.IsInstanceOf<SmartStack<string>>(smartStack);
        }

        [Test]
        public void Reduce_WithABC_ReturnsABCList()
        {
            //Arrange
            ImmuStack<string> stack = ImmuStack<string>.Empty()
                .Push("a")
                .Push("b")
                .Push("c");

            //Act
            List<string> list = SmartStack<string>.Of(stack)
                .Reduce<List<string>>(
                    new List<string>(),                               //init accumulator
                    (acc, item) => { acc.Add(item); return acc; });   //build accumulator

            //Assert
            //todo
            Logger.Debug("list:\r\n" + ToString(list));
            Assert.AreEqual("a", list.ElementAt(0));
            Assert.AreEqual("b", list.ElementAt(1));
            Assert.AreEqual("c", list.ElementAt(2));
        }

        [Test]
        public void Map_WithABC_ReturnsABCList()
        {
            //Arrange
            IEnumerable<string> list1 = new List<string>() { "a", "b", "c" };

            //Act
            IEnumerable<string> list2 = SmartStack<string>.Of(list1)
                .Map<string>((item) => item + "_smart")
                .ToList();


            //Assert
            //todo
            Logger.Debug("list1:\r\n" + ToString(list1));
            Logger.Debug("list2:\r\n" + ToString(list2));
            Assert.AreEqual("a_smart", list2.ElementAt(0));
            Assert.AreEqual("b_smart", list2.ElementAt(1));
            Assert.AreEqual("c_smart", list2.ElementAt(2));
        }

        [Test]
        public void Filter_WithAandBs_ReturnsOnlyAs()
        {
            //Arrange
            IEnumerable<string> list1 = new List<string>() { "a1", "a2", "b1", "b2" };

            //Act
            IEnumerable<string> list2 = SmartStack<string>.Of(list1)
                .Filter((item) => item.StartsWith("a"))
                .ToList();


            //Assert
            //todo
            Logger.Debug("list1:\r\n" + ToString(list1));
            Logger.Debug("list2:\r\n" + ToString(list2));
            Assert.AreEqual(2, list2.Count());
            Assert.AreEqual("a1", list2.ElementAt(0));
            Assert.AreEqual("a2", list2.ElementAt(1));
        }

        [Test]
        public void MapAndFilter_WithAandBs_ReturnsOnlyAs()
        {
            //Arrange
            IEnumerable<string> list1 = new List<string>() { "a1", "a2", "b1", "b2" };

            //Act
            IEnumerable<string> list2 = SmartStack<string>.Of(list1)
                .Map((item) => item + "_smart")
                .Filter((item) => item.StartsWith("a"))
                .ToList();


            //Assert
            //todo
            Logger.Debug("list1:\r\n" + ToString(list1));
            Logger.Debug("list2:\r\n" + ToString(list2));
            Assert.AreEqual(2, list2.Count());
            Assert.AreEqual("a1_smart", list2.ElementAt(0));
            Assert.AreEqual("a2_smart", list2.ElementAt(1));
        }

        #region helpers

        private static string ToString(IEnumerable<string> list, string sep = "\r\n")
        {
            return list.Aggregate((acc, item) => acc + sep + item);
        }

        #endregion
    }
}
