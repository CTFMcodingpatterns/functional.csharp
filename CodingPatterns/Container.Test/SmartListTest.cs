﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using log4net;
using Business;
using Model;

namespace Container.Test
{
    [TestFixture]
    class SmartListTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SmartListTest));

        [Test]
        public void Map_1Element_ReturnsSmartList()
        {
            //Arrange
            SmartList<string> smartList = SmartList<string>.Of(new List<string>() { "alfa", "beta", "gamma" });

            //Act
            ISmartContainer<string> resultList = smartList
                .Map(element => element + "_smart");

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotNull(resultList);
            Assert.AreEqual("\r\nalfa_smart\r\nbeta_smart\r\ngamma_smart", resultList.ToString());
        }

        [Test]
        public void Map_2Elements_ReturnsSmartList()
        {
            //Arrange
            SmartList<string> smartList = SmartList<string>.Of(new List<string>() { "alfa", "beta", "gamma" });

            //Act
            ISmartContainer<string> resultList = smartList
                .Map(element => element + "_smart")
                .Map(el2 => el2.Replace("_", "-"));

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotNull(resultList);
            Assert.AreEqual("\r\nalfa-smart\r\nbeta-smart\r\ngamma-smart", resultList.ToString());
        }

        [Test]
        public void Map_IntElements_ReturnsSmartList()
        {
            //Arrange

            //Act
            ISmartContainer<string> resultList = SmartList<int>.Of(new List<int>() { 10, 20, 30 })
                .Map(num => num * 2)
                .Map(num => num.ToString())
                .Map(str1 => str1 + "_smart")
                .Map(str2 => str2.Replace("_", "-"));

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotNull(resultList);
            Assert.AreEqual("\r\n20-smart\r\n40-smart\r\n60-smart", resultList.ToString());
        }

        [Test]
        public void FilterAndMap_IntElements_ReturnsSmartList()
        {
            //Arrange

            //Act
            ISmartContainer<string> resultList = SmartList<int>
                .Of(new List<int>() { 1, 2, 3, 4 })
                .Filter(num => num % 2 == 0)
                .Map(num => num.ToString())
                .Map(str1 => str1 + "_smart");

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotNull(resultList);
            Assert.AreEqual("\r\n2_smart\r\n4_smart", resultList.ToString());
        }

        [Test]
        public void Map_ElementsNested_ReturnsSmartList()
        {
            //Arrange
            IEnumerable<IEnumerable<string>> nestedList = new List<IEnumerable<string>>() {
                new List<string>() { "a1", "a2", "a3"},
                new List<string>() { "b1", "b2", "b3"},
            };

            //Act
            ISmartContainer<IEnumerable<string>> resultList = SmartList<IEnumerable<string>>
                .Of(nestedList)
                .Map(outerElement => outerElement);

            //Assert
            Logger.Debug("resultList: " + resultList);
            //TODO
        }

        [Test]
        public void FlatMap_ElementsNested_ReturnsSmartList1()
        {
            //Arrange
            IEnumerable<IEnumerable<string>> nestedList = new List<IEnumerable<string>>() {
                new List<string>() { "a1", "a2", "a3"},
                new List<string>() { "b1", "b2", "b3"},
            };

            //Act
            ISmartContainer<string> resultList = SmartList<IEnumerable<string>>
                .Of(nestedList)
                .FlatMap(outerElement => outerElement);

            //Assert
            Logger.Debug("resultList: " + resultList);
            //TODO
        }

        [Test]
        public void FlatMap_ElementsNested_ReturnsSmartList2()
        {
            //Arrange
            IEnumerable<Order> orderList = new List<Order>() {
                new Order(1, "order1", new DateTime(2016,01,01), new List<OrderItem>() {
                    new OrderItem(11, "item11"),
                    new OrderItem(12, "item12")
                }),
                new Order(2, "order2", new DateTime(2016,01,02), new List<OrderItem>() {
                    new OrderItem(21, "item21"),
                    new OrderItem(22, "item22")
                })
            };

            //Act
            ISmartContainer<string> resultList = SmartList<Order>
                .Of(orderList)
                .FlatMap<OrderItem>(order => order.Items)
                .Map(item => item.Description);

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotEmpty(ToList(resultList));
            Assert.AreEqual("item11", ToList(resultList)[0]);
        }

        [Test]
        public void FlatMap_ElementsNested_ReturnsSmartList3()
        {
            //Arrange
            IEnumerable<Order> orderList = new List<Order>() {
                new Order(1, "order1", new DateTime(2016,01,01), new List<OrderItem>() {
                    new OrderItem(11, "item11"),
                    new OrderItem(12, "item12")
                }),
                new Order(2, "order2", new DateTime(2016,01,02), new List<OrderItem>() {
                    new OrderItem(21, "item21"),
                    new OrderItem(22, "item22")
                })
            };

            //Act
            ISmartContainer<string> resultList = SmartList<Order>
                .Of(orderList)
                .FlatMap<OrderItem, string>(
                    order => order.Items,
                    (order, item) => order.Description + ": " + item.Description);

            //Assert
            Logger.Debug("resultList: " + resultList);
            //TODO
        }

        [Test]
        public void Map_WithItems_ReturnsList()
        {
            //Arrange

            //Act
            IEnumerable<OrderItem> itemList = CreateItems();
            IEnumerable<string> priceList = SmartList<OrderItem>.Of(itemList)
                .Filter(item => item.Price < 100)
                .Map(item => item.Number + ". " + item.Description + ": " + item.Price)
                .Reduce(new List<string>(), (acc, item) => { acc.Add(item); return acc; });

            //Assert
            Logger.Debug("priceList: " + priceList.ToString());
        }

        [Test]
        public void LINQSelect_WithItems_ReturnsList()
        {
            //Arrange

            //Act
            IEnumerable<string> priceList = CreateItems()
                .Where(item => item.Price < 100)
                .Select(item => item.Number + ". " + item.Description + ": " + item.Price)
                .ToList();

            //Assert
            Logger.Debug("priceList: " + priceList.ToString());
        }


        #region helpers

        private List<string> ToList(ISmartContainer<string> smartList)
        {
            List<string> descList = smartList.Reduce<List<string>>(
                new List<string>(),
                (acc, el) => { acc.Add(el); return acc; });
            return descList;
        }

        private static IEnumerable<OrderItem> CreateItems()
        {
            IEnumerable<OrderItem> itemList = new List<OrderItem>() {
                new OrderItem(1, "apple", price: 10),
                new OrderItem(2, "banana", price: 20),
                new OrderItem(3, "pea", price: 15),
                new OrderItem(4, "caviar", price: 115),
                new OrderItem(5, "lobster", price: 105)
            };
            return itemList;
        }

        #endregion
    }
}
