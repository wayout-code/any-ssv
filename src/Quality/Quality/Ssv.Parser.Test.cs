namespace Wayout.AnySsv.Quality
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem(@"data\", @"data\")]
    public class SsvParserTest
    {
        [TestMethod]
        public void ParseValidSingleTablesWithDataOnly()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*data_only*[*]-valid.*sv");
            foreach (var file in files)
            {
                Console.Write(file);
                var originalContent = File.ReadAllLines(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                SsvAssertHelper.AssertSsvEqualsContentLines(ssv, originalContent);

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseValidSingleTablesWithTableName()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*name*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllLines(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                SsvAssertHelper.AssertSsvEqualsContentLines(ssv, originalContent);

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseValidSingleTablesWithHeader()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*header*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllLines(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                SsvAssertHelper.AssertSsvEqualsContentLines(ssv, originalContent);
                Assert.AreEqual(Ssv.LineType.Header, originalContent[0]);
                //todo kontrolovat typ radky

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseValidMultiTables()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "multitable-*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllLines(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                SsvAssertHelper.AssertSsvEqualsContentLines(ssv, originalContent);

                Console.WriteLine(" - ok");
            }
        }
    }
}
