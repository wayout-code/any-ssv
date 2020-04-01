namespace Wayout.AnySsv.Quality
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SsvTest
    {
        [TestMethod]
        public void InsertDataLine()
        {
            var ssv = new Ssv();
            ssv.InsertDataLine("a1", "b1");
            
            Assert.AreEqual(1, ssv.Lines.Count);
            Assert.AreEqual("a1", ssv.Lines[0].Values[0]);
            Assert.AreEqual("b1", ssv.Lines[0].Values[1]);
        }

        [TestMethod]
        public void InsertCustomHeaderLine()
        {
            var ssv = new Ssv();
            ssv.InsertHeaderColumnsLine("A","B");

            Assert.AreEqual(1, ssv.Lines.Count);
            Assert.AreEqual("A", ssv.Lines[0].Values[0]);
            Assert.AreEqual("B", ssv.Lines[0].Values[1]);
        }

        //[TestMethod]
        //public void RemoveTable()
        //{
        //    var ssv = ContentHelper.CreateMultiTable(2, 2, 2);

        //    Assert.AreEqual(2, ssv.GetTablesCount());

        //    ssv.RemoveTable(0);

        //    Assert.AreEqual(1, ssv.GetTablesCount());
        //}
    }
}
