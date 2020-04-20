namespace Wayout.AnySsv.Quality
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DynamicObjectParserTest
    {
        [TestMethod]
        public void ParseSigleLineAsObject()
        {
            var parser = new DynamicObjectParser();
            var ssv = SsvFactory.Create3ColumnsNDataLines(1);
            
            dynamic dynObj = parser.Parse(ssv).First();

            Assert.AreEqual("a1", dynObj.A);
            Assert.AreEqual("b1", dynObj.B);
            Assert.AreEqual("c1", dynObj.C);
        }

        [TestMethod]
        public void Parse3LineAsObjects()
        {
            var parser = new DynamicObjectParser();
            var ssv = SsvFactory.Create3ColumnsNDataLines(3, "T");

            var dynObjects = parser.Parse(ssv).ToArray();

            Assert.AreEqual(3, dynObjects.Count());
            dynamic dynObj3 = dynObjects[2];
            Assert.AreEqual("a3", dynObj3.A);
            Assert.AreEqual("b3", dynObj3.B);
            Assert.AreEqual("c3", dynObj3.C);
        }
    }
}
