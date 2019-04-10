namespace Wayout.Ssv.Quality
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SsvFormatterTest
    {
        [TestMethod]
        public void FormatSsvByDefaultNotation()
        {
            var ssv = new Ssv();
            ssv.InsertTableNameLine("T");
            ssv.InsertHeaderColumnsLine("A", "B", "C");
            ssv.InsertDataLine("A1", "B1", "C1");
            ssv.InsertDataLine("A2", "B2", "C2");

            var formatter = new SsvFormatter();
            var format = formatter.Format(ssv);

            var lines = format.Split(new[] { formatter.SsvNotation.LineDelimiter }, StringSplitOptions.None).ToArray();
            Assert.AreEqual(4, lines.Length);
            Assert.AreEqual("[[[T]]]", lines[0]);
            Assert.AreEqual("[[A;B;C]]", lines[1]);
            Assert.AreEqual("A1;B1;C1", lines[2]);
        }

        [TestMethod]
        public void FormatSsvByCustomNotation()
        {
            var ssv = new Ssv();
            ssv.InsertTableNameLine("T");
            ssv.InsertHeaderColumnsLine("A", "B", "C");
            ssv.InsertDataLine("A1", "B1", "C1");
            ssv.InsertDataLine("A2", "B2", "C2");

            var notation = new Notation()
            {
                ValueDelimiter = " || ",
                TableStartMark = "<<",
                TableEndMak = ">>",
                HeaderStartMark = "{{",
                HeaderEndMark = "}}"
            };
            
            var formatter = new SsvFormatter() {SsvNotation = notation};
            var format = formatter.Format(ssv);

            var lines = format.Split(new[] { formatter.SsvNotation.LineDelimiter }, StringSplitOptions.None).ToArray();
            Assert.AreEqual(4, lines.Length);
            Assert.AreEqual("<<T>>", lines[0]);
            Assert.AreEqual("{{A || B || C}}", lines[1]);
            Assert.AreEqual("A1 || B1 || C1", lines[2]);
        }
    }
}
