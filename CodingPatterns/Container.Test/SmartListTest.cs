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
            SmartList<string> sList = new SmartList<string>(new List<string>() { "alfa", "beta", "gamma" });

            //Act
            SmartList<string> resultList = sList
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
            SmartList<string> sList = new SmartList<string>(new List<string>() { "alfa", "beta", "gamma" });

            //Act
            SmartList<string> resultList = sList
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
            SmartList<int> intList = new SmartList<int>(new List<int>() { 10, 20, 30 });

            //Act
            SmartList<string> resultList = intList
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
            SmartList<int> intList = new SmartList<int>(new List<int>() { 1, 2, 3, 4 });

            //Act
            SmartList<string> resultList = intList
                .Filter(num => num % 2 == 0)
                .Map(num => num.ToString())
                .Map(str1 => str1 + "_smart");

            //Assert
            Logger.Debug("resultList: " + resultList);
            Assert.IsNotNull(resultList);
            Assert.AreEqual("\r\n2_smart\r\n4_smart", resultList.ToString());
        }
    }
}
