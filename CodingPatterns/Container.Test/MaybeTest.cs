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
    class MaybeTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaybeTest));

        [Test]
        public void Map_hello_hella()
        {
            //Arrange
            Maybe<string> opt = new Maybe<string>("hello");

            //Act
            Maybe<string> optResult = opt.Map(str => str.Replace("o", "a"));

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual("hella", optResult.Get());
        }

        [Test]
        public void Map_hello_henna()
        {
            //Arrange
            Maybe<string> opt = new Maybe<string>("hello");

            //Act
            Maybe<string> optResult = opt
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
            Maybe<string> opt = new Maybe<string>(null);

            //Act
            Maybe<string> optResult = opt
                .Map(str => str.Replace("o", "a"))
                .Map(str => str.Replace("l", "n"));

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual(Maybe<string>.EMPTY, optResult);
        }

        [Test]
        public void Map_null2_empty()
        {
            //Arrange
            Maybe<string> opt = new Maybe<string>("hello");

            //Act
            Maybe<string> optResult = opt           //value = "hello"
                .Map(str => str.Replace("o", "a"))  //value = "hella"
                .Map<string>(str => null)           //value = null
                .Map(str => str.Replace("l", "n")); //value = null

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual(Maybe<string>.EMPTY, optResult);
        }

        [Test]
        public void SelectMap_hello_hella()
        {
            //Arrange
            Maybe<string> opt1 = new Maybe<string>("hello");
            Maybe<string> opt2 = new Maybe<string>(null);
            IEnumerable<Maybe<string>> strList = new List<Maybe<string>>() {
                opt1, opt2
            };

            //Act
            Maybe<string> optResult = strList
                .Select(opt => opt.Map(str => str.Replace("o", "a")))
                .Select(opt => opt.Map(str => str.Replace("l", "n")))
                .First();

            //Assert
            Logger.Debug("optResult: " + optResult.Get());
            Assert.AreEqual("henna", optResult.Get());
        }

        [Test]
        public void Map_NullTransformed_empty()
        {
            //Arrange
            Maybe<string> opt = new Maybe<string>("hello");

            //Act
            Maybe<string> optResult = opt               //value = "hello"
                .Map(str => str.Replace("o", "a"))      //value = "hella"
                .Map<string>(str => TransformVal(str))  //value = null
                .Map(str => str.Replace("l", "n"));     //value = null

            //Assert
            Logger.Debug("result: " + optResult.Get());
            Assert.AreEqual(Maybe<string>.EMPTY, optResult);
        }

        [Test]
public void RawValueOperation_hello_hella()
{
    //Arrange
    string val0 = "hello";

    //Act
    string val1 = val0.Replace("o", "a");
    string val2 = null;
    string val3 = null;
    if (val1 != null) {                      //val1 = "hella"
        val2 = TransformVal(val1);           //val2 = null
        if (val2 != null) {
            val3 = val2.Replace("l", "n");   //val3 = null
        }
    }

    //Assert
    Logger.Debug(""
        + "\r\nval1: " + val1
        + "\r\nval2: " + val2
        + "\r\nval3: " + val3);
    Assert.AreEqual(null, val3);
}

private string TransformVal(string valIn)
{
    return null;
}
    }
}
