namespace Wayout.AnySsv
{
    using System;

    public class Notation
    {
        public static class Default
        {
            public const string ValueDelimiter = ";";
            public const string TableStartMark = "[[[";
            public const string TableEndMak = "]]]";
            public const string HeaderStartMark = "[[";
            public const string HeaderEndMark = "]]";
            public const string LineExclusionMark = "#";
        }

        public Notation()
        {
            ValueDelimiter = Default.ValueDelimiter;
            LineDelimiter = Environment.NewLine;
            TableStartMark = Default.TableStartMark;
            TableEndMak = Default.TableEndMak;
            HeaderStartMark = Default.HeaderStartMark;
            HeaderEndMark = Default.HeaderEndMark;
            LineExclusionMark = Default.LineExclusionMark;
        }

        public string ValueDelimiter { get; set; }

        public string LineDelimiter { get; set; }

        public string TableStartMark { get; set; }
        public string TableEndMak { get; set; }

        public string HeaderStartMark { get; set; }
        public string HeaderEndMark { get; set; }

        public string LineExclusionMark { get; set; }
    }
}
