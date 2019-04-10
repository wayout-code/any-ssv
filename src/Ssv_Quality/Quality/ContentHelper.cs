namespace Wayout.Ssv.Quality
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal static class ContentHelper
    {
        public static string RootDataFolder => Path.Combine("Quality", "data");

        public static string[] LoadContentLines(string fileName) => File.ReadAllLines(Path.Combine(RootDataFolder, fileName));

        public static string LoadContent(string fileName) => File.ReadAllText(Path.Combine(RootDataFolder, fileName));

        public static List<string[]> SplitContentToValues(IList<string> contentLines, string separator)
        {
            var dataLines = new List<string[]>();
            foreach (var line in contentLines)
            {
                var data = line.Split(separator);
                dataLines.Add(data);
            }

            return dataLines;
        }

        public static List<string[]> SplitContentToValues(string content, string separator)
        {
            var contentLines = content.Split(Environment.NewLine);
            return SplitContentToValues(contentLines, separator);
        }

        public static IEnumerable<Ssv.Line> CreateFullHeader(int columns)
        {
            var offsets = Enumerable.Range(0, columns);
            var collumnNames = offsets.Select(x => (char)(((int)'A') + x));
            yield return new Ssv.Line(collumnNames.Select(x => x.ToString()).ToArray());
        }

        public static IEnumerable<Ssv.Line> CreateDataLines(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                var line = new Ssv.Line();
                for (int j = 0; j < columns; j++)
                {
                    var columnValue = (char)(((int)'a') + j);
                    line.Values.Add($"{columnValue}{i}");
                }

                yield return line;
            }
        }

        public static Ssv CreateMultiTable(int tables, int rows, int columns)
        {
            var ssv = new Ssv();
            for (int i = 0; i < tables; i++)
            {
                ssv.InsertTableNameLine("T " + i);
                foreach (var headerLine in CreateFullHeader(3))
                    ssv.Insert(headerLine);
                foreach (var dataLine in CreateDataLines(rows, columns))
                    ssv.Insert(dataLine);
            }

            return ssv;
        }
    }
}
