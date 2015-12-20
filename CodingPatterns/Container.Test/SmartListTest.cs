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
    class SmartListTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SmartListTest));

        [Test]
        public void Map_1Element_ReturnsSmartList()
        {
            //Arrange
            SmartList<string> smartList = SmartList<string>.Of(new List<string>() { "alfa", "beta", "gamma" });

            //Act
            SmartList<string> resultList = smartList
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
            SmartList<string> resultList = smartList
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
            SmartList<string> resultList = SmartList<int>.Of(new List<int>() { 10, 20, 30 })
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
            SmartList<string> resultList = SmartList<int>
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
        public void FlatMap_ElementsNested_ReturnsSmartList()
        {
            //Arrange
            IEnumerable<IEnumerable<string>> nestedList = new List<IEnumerable<string>>() {
                new List<string>() { "a1", "a2", "a3"},
                new List<string>() { "b1", "b2", "b3"},
            };
            var result = nestedList.First();

            //Act
            SmartList<string> resultList = SmartList<IEnumerable<string>>
                .Of(nestedList)
                .FlatMap(outer => outer);

            //Assert
            Logger.Debug("resultList: " + resultList);
            //TODO
        }

        [Test]
        public void FlatMap_ElementsNested_ReturnsSmartList2()
        {
            //Arrange
            IEnumerable<IEnumerable<string>> nestedList = new List<IEnumerable<string>>() {
                new List<string>() { "a1", "a2", "a3"},
                new List<string>() { "b1", "b2", "b3"},
            };
            var result = nestedList.First();

            //Act
            SmartList<string> resultList = SmartList<IEnumerable<string>>
                .Of(nestedList)
                .FlatMap(outer => outer, (outer, inner) => outer + ": " + inner);

            //Assert
            //TODO
        }

    }
}
