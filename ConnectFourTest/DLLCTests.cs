using NUnit.Framework;
using Ksu.Cis300.ConnectFour;
using System;

namespace Tests
{
    public class DLLCTests
    {

        [Test]
        [Category("A-DoubleLinkList")]
        public void A1_DLLCUnitTest_newCell()
        {
            DoubleLinkedListCell<int> cell = new DoubleLinkedListCell<int>("cat");
            Assert.AreEqual("cat", cell.Id);
            Assert.AreEqual(null, cell.Prev);
            Assert.AreEqual(null, cell.Next);
        }

        [Test]
        [Category("A-DoubleLinkList")]
        public void A2_DLLCUnitTest_NextPrev()
        {
            DoubleLinkedListCell<int> cell = new DoubleLinkedListCell<int>("cat");
            DoubleLinkedListCell<int> cell2 = new DoubleLinkedListCell<int>("dog");
            cell.Data = 5;
            cell2.Data = 10;
            Assert.AreEqual(5, cell.Data);
            Assert.AreEqual(10, cell2.Data);
            cell.Next = cell2;
            cell2.Prev = cell;
            Assert.AreEqual(cell, cell2.Prev);
            Assert.AreEqual(cell2, cell.Next);
        }



    }
}