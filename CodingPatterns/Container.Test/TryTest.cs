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
    public class TryTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TryTest));

        [Test]
        public void Map_WithHelloString_ReturnsHelloWorld()
        {
            //Arrange
            Try<string> tryHello = Try<string>.OfValue("hello");

            //Act
            var result = tryHello.Map<string>(s => s + " world");

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual("hello world", result.Value);
        }

        [Test]
        public void Map_WithNumberString_ReturnsInteger()
        {
            //Arrange
            Try<string> try42 = Try<string>.OfValue("42");

            //Act
            Try<int> result = try42.Map<int>(s => Int32.Parse(s));

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(42, result.Value);
        }

        [Test]
        public void Map_WithNumberString_ReturnsError()
        {
            //Arrange
            Try<string> try42 = Try<string>.OfValue("42xxx");

            //Act
            Try<int> result = try42.Map<int>(s => Int32.Parse(s));

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(default(int), result.Value);
            Assert.IsInstanceOf<FormatException>(result.Error);
        }

        [Test]
        public void Map_WithZeroString_ReturnsValue()
        {
            //Arrange
            Try<string> try42 = Try<string>.OfValue("42");

            //Act
            Try<int> result = try42
                .Map<string>(s => s + "00")
                .Map<int>(s => Int32.Parse(s));

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(4200, result.Value);
            Assert.AreEqual(default(Exception), result.Error);
        }

        [Test]
        public void Map_WithNumberStringCalc_ReturnsValue()
        {
            //Arrange
            Try<string> try42 = Try<string>.OfValue("42");

            //Act
            Try<int?> result = try42             // value = "42"
                .Map<string>(s => s + "00")      // value = "4200"
                .Map<int?>(s => Int32.Parse(s))  // value = 4200
                .Map<int?>(i => i * 2);          // value = 8400

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(8400, result.Value);
            Assert.AreEqual(default(Exception), result.Error);
        }

        [Test]
        public void Map_WithNumberStringCalc_ReturnsError()
        {
            //Arrange
            Try<string> try42 = Try<string>.OfValue("42");

            //Act
            Try<int?> result = try42              // value = "42"
                .Map<string>(s => s + "xx")       // value = "42xx"
                .Map<int?>(s => Int32.Parse(s))   // value = null, err = FormatException
                .Map<int?>(i => i * 2);           // value = null, err = FormatException

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(default(int?), result.Value);
            Assert.IsInstanceOf<FormatException>(result.Error);
        }

        [Test]
        public void Map_WithNumberZeroDivision_ReturnsError()
        {
            //Arrange
            Try<int?> try0 = Try<int?>.OfValue(0);

            //Act
            Try<int?> result = try0                  // value = 0,    err = null
                .Map<int?>(i => 2 / i)               // value = null, err = ZeroException
                .Map<string>(i => i + "xx")          // value = null, err = ZeroException
                .Map<int?>(str => Int32.Parse(str)); // value = null, err = ZeroException

            //Assert
            Logger.Debug("result: " + result);
            Assert.AreEqual(default(int?), result.Value);
            Assert.IsInstanceOf<DivideByZeroException>(result.Error);
        }
    }
}
