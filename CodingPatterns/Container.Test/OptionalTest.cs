using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using NUnit.Framework;


namespace Container.Test
{
    [TestFixture]
    class OptionalTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OptionalTest));

        [Test]
        public void Map_hello_hella()
        {
            //Arrange
            Optional<string> opt = new Optional<string>("hello");

            //Act
            Optional<string> optResult = opt.Map(str => str.Replace("o", "a"));

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual("hella", optResult.Get());
        }

        [Test]
        public void Map_hello_henna()
        {
            //Arrange
            Optional<string> opt = new Optional<string>("hello");

            //Act
            Optional<string> optResult = opt
                .Map(str => str.Replace("o", "a"))
                .Map(str => str.Replace("l", "n"));

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual("henna", optResult.Get());
        }

        [Test]
        public void Map_null_empty()
        {
            //Arrange
            Optional<string> opt = new Optional<string>(null);

            //Act
            Optional<string> optResult = opt
                .Map(str => str.Replace("o", "a"))
                .Map(str => str.Replace("l", "n"));

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual(Optional<string>.EMPTY, optResult);
        }

        [Test]
        public void Map_null2_empty()
        {
            //Arrange
            Optional<string> opt = new Optional<string>("hello");

            //Act
            Optional<string> optResult = opt        //value = "hello"
                .Map(str => str.Replace("o", "a"))  //value = "hella"
                .Map<string>(str => null)           //value = null
                .Map(str => str.Replace("l", "n")); //value = null

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual(Optional<string>.EMPTY, optResult);
        }


        [Test]
        public void SelectMap_hello_hella()
        {
            //Arrange
            Optional<string> opt1 = new Optional<string>("hello");
            Optional<string> opt2 = new Optional<string>(null);
            IEnumerable<Optional<string>> strList = new List<Optional<string>>() {
                opt1, opt2
            };

            //Act
            Optional<string> optResult = strList
                .Select(opt => opt.Map(str => str.Replace("o", "a")))
                .Select(opt => opt.Map(str => str.Replace("l", "n")))
                .First();

            //Assert
            Logger.Debug("optResult: " + optResult.Get());
            Assert.AreEqual("henna", optResult.Get());
        }

    }
}
