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
        public void Push_AB_ReturnsBA()
        {
            //Arrange
            ImmuStack<string> stack0 = ImmuStack<string>.Empty();

            //Act
            ImmuStack<string> stack1 = stack0.Push("a");
            ImmuStack<string> stack2 = stack1.Push("b");

            //Assert
            Logger.Debug("stack2:\r\n" + stack2);
            Assert.AreEqual("b", stack2.Head);
        }

        [Test]
        public void FoldLeft_PushToAB_ReturnsBA()
        {
            //Arrange
            ImmuStack<string> stack0 = ImmuStack<string>.Empty()
                .Push("b")
                .Push("a");

            //Act
            ImmuStack<string> stack1 = stack0
                .FoldLeft(ImmuStack<string>.Empty(), (acc, item) => acc.Push(item));

            //Assert
            Logger.Debug(""
                + "\r\nstack0:\r\n" + stack0
                + "\r\nstack1:\r\n" + stack1);
            Assert.AreEqual("b", stack1.Head);
        }

        [Test]
        public void FoldRight_PushToAB_ReturnsAB()
        {
            //Arrange
            ImmuStack<string> stack0 = ImmuStack<string>.Empty()
                .Push("b")   //second
                .Push("a");  //head

            //Act
            ImmuStack<string> stack1 = stack0
                .FoldRight(ImmuStack<string>.Empty(), (acc, item) => acc.Push(item));

            //Assert
            Logger.Debug(""
                + "\r\nstack0:\r\n" + stack0
                + "\r\nstack1:\r\n" + stack1);
            Assert.AreEqual("a", stack1.Head);
        }

        [Test]
        public void ToString4_PushToABC_ReturnsABC()
        {
            //Arrange
            ImmuStack<string> stack = ImmuStack<string>.Empty()
                .Push("c")   //third
                .Push("b")   //second
                .Push("a");  //first = head

            //Act
            string stackString = stack.ToString();

            //Assert
            Logger.Debug(""
                + "\r\nstack0:\r\n" + stack);
            Assert.AreEqual("a\r\nb\r\nc", stackString);
        }

        [Test]
        public void FomList_WithListABC_ReturnsStackABC()
        {
            //Arrange
            IEnumerable<string> list = new List<string>() { "a", "b", "c" };

            //Act
            ImmuStack<string> stack = ImmuStack<string>.FromList(list);

            //Assert
            Logger.Debug(""
                + "\r\nstack0:\r\n" + stack);
            Assert.AreEqual("a", stack.Head);
        }

        [Test]
        public void ToList_WithStack_ReturnsList()
        {
            //Arrange
            ImmuStack<string> stack = ImmuStack<string>.Empty()
                .Push("c")   //third
                .Push("b")   //second
                .Push("a");  //first = head

            //Act
            IEnumerable<string> list = stack.ToList();

            //Assert
            Logger.Debug(""
                + "\r\nstack0:\r\n" + stack);
            Assert.AreEqual(new List<string>() { "a", "b", "c" }, list);
        }

        [Test]
        public void ToList_WithListABC_ReturnsListABC()
        {
            //Arrange
            IEnumerable<string> list1 = new List<string>() { "a", "b", "c" };
            ImmuStack<string> stack = ImmuStack<string>.FromList(list1);

            //Act
            IEnumerable<string> list2 = stack.ToList();

            //Assert
            Logger.Debug(""
                + "\r\nlist2:\r\n" + ToString(list2));
            Assert.AreEqual(new List<string>() { "a", "b", "c" }, list2);
        }

        #region helpers

        private static string ToString(IEnumerable<string> list, string sep = "\r\n")
        {
            return list.Aggregate((acc, item) => acc + sep + item);
        }

        #endregion

    }
}
