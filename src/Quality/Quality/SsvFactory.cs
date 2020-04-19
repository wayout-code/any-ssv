namespace Wayout.AnySsv.Quality
{
    public static class SsvFactory
    {
        public static Ssv Create3ColumnsNDataLines(int n, string tableName = null)
        {
            var ssv = new Ssv();
            if (tableName != null)
                ssv.InsertTableNameLine(tableName);

            ssv.InsertHeaderColumnsLine("A", "B", "C");
            for (int i = 1; i <= n; i++)
                ssv.InsertDataLine("a" + i, "b" + i, "c" + i);

            return ssv;
        }

        public static Notation CreateCustomNotation()
        {
            return new Notation()
            {
                ValueDelimiter = " || ",
                TableStartMark = "<<",
                TableEndMak = ">>",
                HeaderStartMark = "{{",
                HeaderEndMark = "}}"
            };
        }
    }
}
