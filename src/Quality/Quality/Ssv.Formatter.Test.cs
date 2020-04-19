namespace Wayout.AnySsv.Quality
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
            var ssv = SsvFactory.Create3ColumnsNDataLines(2, "T");

            var formatter = new SsvFormatter();
            var format = formatter.Format(ssv);

            var lines = format.Split(new[] { formatter.SsvNotation.LineDelimiter }, StringSplitOptions.None).ToArray();
            Assert.AreEqual(4, lines.Length);
            Assert.AreEqual("[[[T]]]", lines[0]);
            Assert.AreEqual("[[A;B;C]]", lines[1]);
            Assert.AreEqual("a1;b1;c1", lines[2]);
        }

        [TestMethod]
        public void FormatSsvByCustomNotation()
        {
            var ssv = SsvFactory.Create3ColumnsNDataLines(2, "T");
            var notation = SsvFactory.CreateCustomNotation();

            var formatter = new SsvFormatter() {SsvNotation = notation};
            var format = formatter.Format(ssv);

            var lines = format.Split(new[] { formatter.SsvNotation.LineDelimiter }, StringSplitOptions.None).ToArray();
            Assert.AreEqual(4, lines.Length);
            Assert.AreEqual("<<T>>", lines[0]);
            Assert.AreEqual("{{A || B || C}}", lines[1]);
            Assert.AreEqual("a1 || b1 || c1", lines[2]);
        }
    }
}
