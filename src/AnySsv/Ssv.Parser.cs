namespace Wayout.AnySsv
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// String to Ssv object parser.
    /// </summary>
    public class SsvParser
    {
        public static class Default
        {
            public static Notation CreateNotation() => new Notation();
        }

        public SsvParser()
        {
            SsvNotation = Default.CreateNotation();
        }

        public Notation SsvNotation { get; set; }

        public Ssv Parse(string content)
        {
            var lines = content.Split(new[] { SsvNotation.LineDelimiter }, StringSplitOptions.None);
            return Parse(lines);
        }

        public Ssv Parse(IEnumerable<string> contentLines)
        {
            var ssv = new Ssv();

            //var previousLine = new Ssv.Line() { LineType = Ssv.LineType.Empty };
            Ssv.Line previousLine = null;
            foreach (var line in contentLines)
            {
                //empty
                if (line == string.Empty)
                {
                    previousLine = ssv.InsertEmptyLine();
                    continue;
                }

                //table
                if (line.TrimStart().StartsWith(SsvNotation.TableStartMark))
                {
                    var name = line.Trim().Replace(SsvNotation.TableStartMark, string.Empty).Replace(SsvNotation.TableEndMak, string.Empty);
                    previousLine = ssv.InsertTableNameLine(name);
                    continue;
                }

                //header
                if (line.TrimStart().StartsWith(SsvNotation.HeaderStartMark))
                {
                    var columnNames = line.Trim().Replace(SsvNotation.HeaderStartMark, string.Empty).Replace(SsvNotation.HeaderEndMark, string.Empty);
                    var columnNamesRow = ParseHeaderRowLine(columnNames, previousLine?.LineType);
                    previousLine = ssv.Insert(columnNamesRow);
                    continue;
                }

                //comment
                if (line.TrimStart().StartsWith(SsvNotation.LineExclusionMark))
                {
                    var coment = line.Trim().Replace(SsvNotation.LineExclusionMark, string.Empty);
                    ssv.Insert(new Ssv.CommentLine(coment));
                    continue;
                }

                //data
                var dataRow = ParseDataLine(line);
                ssv.Insert(dataRow);
            }

            return ssv;
        }

        private Ssv.Line ParseHeaderRowLine(string line, Ssv.LineType? previousLineType)
        {
            var names = line.Split(new[] { SsvNotation.ValueDelimiter }, StringSplitOptions.None);
            if (previousLineType == null || previousLineType != Ssv.LineType.Header)
                return new Ssv.Line(names) { LineType = Ssv.LineType.ColumnsNames };
            else
                return new Ssv.Line(names) { LineType = Ssv.LineType.Header };
        }

        private Ssv.Line ParseDataLine(string line)
        {
            var values = line.Split(new[] { SsvNotation.ValueDelimiter }, StringSplitOptions.None);
            return new Ssv.Line(values) {LineType = Ssv.LineType.Data};
        }
    }
}
