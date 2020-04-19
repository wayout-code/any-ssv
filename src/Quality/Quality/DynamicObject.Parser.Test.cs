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
        public void Parse()
        {
            var parser = new DynamicObjectParser();
            var ssvParser = new SsvParser();

            var files = Directory.GetFiles(ContentHelper.RootDataFolder, "singletable-*header*[*]-valid.ssv");
            foreach (var filez in files)
            {
                var file = files.ElementAt(4);

                Console.Write(file);
                var originalContent = File.ReadAllLines(file);
                var ssv = ssvParser.Parse(originalContent);
                var eobjects = parser.Parse(ssv).ToArray();


                //SsvAssertHelper.AssertSsvEqualsContentLines(ssv, originalContent);
                //Console.WriteLine(" - ok");
            }
        }
    }
}
