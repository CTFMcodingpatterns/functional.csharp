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
    class ImmuStackTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ImmuStackTest));

        [Test]
        public void Cons_abc_HeadIsC()
        {
            //Arrange
            ImmuStack<string> stack0 = ImmuStack<string>.Empty();

            //Act
            ImmuStack<string> stack1 = new ImmuStack<string>("a", stack0);
            ImmuStack<string> stack2 = new ImmuStack<string>("b", stack1);
            ImmuStack<string> stack3 = new ImmuStack<string>("c", stack2);

            //Assert
            Logger.Debug(""
                + "\r\nstack0.head: " + stack0.Head
                + "\r\nstack1.head: " + stack1.Head
                + "\r\nstack2.head: " + stack2.Head
                + "\r\nstack3.head: " + stack3.Head
                );
            Assert.AreEqual(null, stack0.Head);
            Assert.AreEqual("a", stack1.Head);
            Assert.AreEqual("b", stack2.Head);
            Assert.AreEqual("c", stack3.Head);
        }

        [Test]
        public void Push_x_y()
        {
            //Arrange
            ImmuStack<string> stack0 = ImmuStack<string>.Empty();

            //Act
            ImmuStack<string> stack1 = stack0.Push("a");
            ImmuStack<string> stack2 = stack1.Push("b");

            //Assert
            Logger.Debug("stack2: " + stack2.ToString());
            Assert.AreEqual("b", stack2.Head);
        }
    }
}
