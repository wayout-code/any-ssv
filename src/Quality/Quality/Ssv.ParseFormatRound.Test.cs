namespace Wayout.AnySsv.Quality
{
    using System;
    using System.Globalization;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem(@"data\", @"data\")]
    public class SsvParseFormatRoundTest
    {
        [TestMethod]
        public void ParseAndFormatValidSingleTablesWithDataOnly()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*data_only*[*]-valid.*sv");
            foreach (var file in files)
            {
                Console.Write(file);
                var originalContent = File.ReadAllText(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                var format = new SsvFormatter() { SsvNotation = parser.SsvNotation }.Format(ssv);

                Assert.AreEqual(originalContent, format, false, CultureInfo.InvariantCulture, file);

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseAndFormatValidSingleTablesWithTableName()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*name*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllText(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                var format = new SsvFormatter() { SsvNotation = parser.SsvNotation }.Format(ssv);

                Assert.AreEqual(originalContent, format, false, CultureInfo.InvariantCulture, file);

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseAndFormatValidSingleTablesWithHeader()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*header*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllText(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                var format = new SsvFormatter() { SsvNotation = parser.SsvNotation }.Format(ssv);

                Assert.AreEqual(originalContent, format, false, CultureInfo.InvariantCulture, file);

                Console.WriteLine(" - ok");
            }
        }

        [TestMethod]
        public void ParseAndFormatValidMultiTables()
        {
            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "multitable-*[*]-valid.ssv");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var originalContent = File.ReadAllText(file);
                var parser = new SsvParser();
                var ssv = parser.Parse(originalContent);

                var format = new SsvFormatter() { SsvNotation = parser.SsvNotation }.Format(ssv);

                Assert.AreEqual(originalContent, format, false, CultureInfo.InvariantCulture, file);

                Console.WriteLine(" - ok");
            }
        }
    }
}
