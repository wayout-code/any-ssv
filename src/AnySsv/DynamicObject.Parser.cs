namespace Wayout.AnySsv
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    /// <summary>
    /// Ssv format to dynamic object parser.
    /// </summary>
    public class DynamicObjectParser
    {
        public DynamicObjectParser()
        {
        }

        private SsvParser _ssvParser = new SsvParser();

        public IEnumerable<ExpandoObject> ParseData(string format)
        {
            var ssv = _ssvParser.Parse(format);
            return Parse(ssv);
        }

        public IEnumerable<ExpandoObject> Parse(Ssv format)
        {
            var tableNames = format.GetTablesNames().ToArray();

            foreach (var line in format.GetTableData(0))
            {
                IDictionary<string, object> obj = new ExpandoObject();

                var columnNamesLine = format.GetTableColumnNamesLine();
                var columnNames = columnNamesLine.Values.Select(c => c.Trim()).ToArray();

                for (int i = 0; i < line.Values.Count; i++)
                {
                    var v = line.Values[i].Trim();
                    if (v.StartsWith("\"") && v.EndsWith("\""))
                    {
                        obj[columnNames[i]] = v;
                    }
                    else if (int.TryParse(v, out int result))
                    {
                        obj[columnNames[i]] = result;
                    }
                    else if (double.TryParse(v, out double dresult))
                    {
                        obj[columnNames[i]] = dresult;
                    }

                    obj[columnNames[i]] = v;
                }

                yield return (ExpandoObject) obj;
            }
        }
    }
}
