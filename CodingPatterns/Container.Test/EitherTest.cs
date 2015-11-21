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
    class EitherTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EitherTest));

        [Test]
        public void Parse_WithNumberString_ReturnsNumber()
        {
            //Act
            Either<Exception, int> either42 = EitherUser.Parse("42");

            //Assert
            Logger.Debug("either42: " + either42);
            Assert.AreEqual(default(Exception), either42.Left);
            Assert.AreEqual(42, either42.Right);
        }

        [Test]
        public void Parse_WithNumberString_ReturnsError()
        {
            //Act
            Either<Exception, int> either42 = EitherUser.Parse("42xxx");

            //Assert
            Logger.Debug("either42: " + either42);
            Assert.IsInstanceOf<FormatException>(either42.Left);
            Assert.AreEqual(default(int), either42.Right);
        }

        [Test]
        public void ParseAndCalc_WithNumberString_ReturnsNumber()
        {
            //Act
            Either<Exception, int> either42 = EitherUser.Parse("42");
            int num84 = 0;
            if (either42.Right != null)
            {
                num84 = either42.Right * 2;
            }

            //Assert
            Logger.Debug(""
                + "\r\neither42: " + either42
                + "\r\nnum84: " + num84);
            Assert.AreEqual(84, num84);
        }
    }
}
