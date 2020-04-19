namespace Wayout.AnySsv
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// String separated values
    /// </summary>
    public class Ssv
    {
        private readonly IList<Line> _lines;

        public Ssv(IList<Line> lines = null)
        {
            _lines = lines ?? new List<Line>();
            Lines = new ReadOnlyCollection<Line>(_lines);
        }

        public static Ssv Load(string filePath, Notation notation = null)
        {
            var content = File.ReadAllText(filePath);
            var parser = new SsvParser() { SsvNotation = notation };
            
            return parser.Parse(content);
        }

        public static Ssv Load(string filePath, string valueDelimiter)
        {
            var content = File.ReadAllText(filePath);
            var notation = SsvParser.Default.CreateNotation();
            notation.ValueDelimiter = valueDelimiter;
            var parser = new SsvParser() { SsvNotation = notation };
            
            return parser.Parse(content);
        }

        public IReadOnlyList<Line> Lines { get; }

        #region lines

        public Line Insert(Line line)
        {
            _lines.Add(line);
            return line;
        }

        public Line InsertAt(Line line, int lineIndex)
        {
            _lines.Insert(lineIndex, line);
            return line;
        }

        public Line InsertTableNameLine(string name)
        {
            var line = new Line() { Name = name, LineType = LineType.TableName };
            _lines.Add(line);
            return line;
        }

        public Line InsertHeaderColumnsLine(params string[] names)
        {
            var line = new Line(names) { LineType = LineType.ColumnsNames };
            _lines.Add(line);
            return line;
        }

        public Line InsertHeaderDefaultValuesLine(params string[] values)
        {
            var line = new Line(values) { LineType = LineType.DefaultValues };
            _lines.Add(line);
            return line;
        }

        public Line InsertCustomHeaderLine(params string[] values)
        {
            var line = new Line(values) { LineType = LineType.Custom };
            _lines.Add(line);
            return line;
        }

        public Line InsertDataLine(params string[] values)
        {
            var line = new Line(values) { LineType = LineType.Data };
            _lines.Add(line);
            return line;
        }

        public Line InsertCommentLine(string comment)
        {
            var line = new CommentLine(comment) { LineType = LineType.Excluded, IsExcluded = true };
            _lines.Add(line);
            return line;
        }

        public Line InsertEmptyLine()
        {
            var line = new CommentLine() { LineType = LineType.Excluded, IsExcluded = true };
            _lines.Add(line);
            return line;
        }

        public Line Remove(Line line)
        {
            _lines.Remove(line);
            return line;
        }

        public Line RemoveAt(int lineIndex)
        {
            var line = _lines[lineIndex];
            _lines.RemoveAt(lineIndex);
            return line;
        }

        #endregion

        #region tables

        public int GetTablesCount()
        {
            if (_lines.Any())
            {
                var count = _lines.Count(l => l.LineType == LineType.TableName);
                return count == 0 ? 1 : count;
            }
            
            return 0;
        }

        /// <summary>
        /// Gets list of names of tables. If table has no explicit name then string.Empty is returned.
        /// </summary>
        public IEnumerable<string> GetTablesNames()
        {
            return _lines.Where(l => l.LineType == LineType.TableName).Select(l => l.Name);
        }

        public IEnumerable<Line> GetTable(int tableIndex = 0)
        {
            return FindTableByIndex(tableIndex, out int tableStartLineIndex)
                ? GetTableLinesByLineIndex(tableStartLineIndex)
                : null;
        }

        public IEnumerable<Line> GetTable(string tableName)
        {
            return FindTableByName(tableName, out int tableStartLineIndex) 
                ? GetTableLinesByLineIndex(tableStartLineIndex) 
                : null;
        }

        public IEnumerable<Line> GetTableData(int tableIndex = 0)
        {
            return tableIndex == 0
                ? _lines.Where(l => l.LineType == LineType.Data)
                : null;
        }

        public IEnumerable<Line> GetTableData(string tableName)
        {
            return FindTableByName(tableName, out var lineIndex, out var tableIndex)
                ? GetTableData(lineIndex)
                : null;
        }

        protected IEnumerable<Line> GetTableLinesByLineIndex(int tableStartLineIndex)
        {
            if (FindTable(tableStartLineIndex + 1, out int nextTableStartLineIndex))
            {
                for (int i = tableStartLineIndex; i < nextTableStartLineIndex; i++)
                    yield return _lines[i];
            }
            else
            {
                for (int i = tableStartLineIndex; i < _lines.Count; i++)
                    yield return _lines[i];
            }
        }

        public Line GetTableColumnNamesLine(int tableIndex = 0)
        {
            return tableIndex == 0
                ? _lines.FirstOrDefault(l => l.LineType == LineType.ColumnsNames)
                : null;
        }

        public IEnumerable<Line> RemoveTable(int tableIndex)
        {
            return null;
        }

        public IEnumerable<Line> RemoveTable(string tableName)
        {
            return null;
        }

        public bool FindTable(int startLineIndex, out int tableLineIndex)
        {
            tableLineIndex = -1;
            for (int i = startLineIndex; i < _lines.Count; i++)
            {
                if (_lines[i].LineType == LineType.TableName)
                {
                    tableLineIndex = i;
                    return true;
                }
            }

            return false;
        }

        public bool FindTableByIndex(int tableIndex, out int tableLineIndex)
        {
            tableLineIndex = -1;
            int tableCounter = 0;
            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].LineType == LineType.TableName)
                {
                    tableCounter++;
                    if (tableCounter == tableIndex)
                    {
                        tableLineIndex = i;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindTableByName(string tableName, out int tableLineIndex, out int tableIndex)
        {
            tableLineIndex = -1;
            tableIndex = -1;
            //int tableCounter = 0;
            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].LineType == LineType.TableName)
                {
                    //tableCounter++;
                    if (_lines[i].Name == tableName)
                    {
                        tableLineIndex = i;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindTableByName(string tableName, out int tableLineIndex, int startLineIndex = 0)
        {
            tableLineIndex = -1;
            for (int i = startLineIndex; i < _lines.Count; i++)
            {
                if (_lines[i].LineType == LineType.TableName && _lines[i].Name == tableName)
                {
                    tableLineIndex = i;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region line classes

        public class Line
        {
            public Line(IList<string> values)
            {
                Values = values ?? new List<string>();
            }
            public Line(params string[] values)
            {
                Values = values;
            }

            public string Name { get; set; }

            public LineType LineType { get; set; }

            public IList<string> Values { get; private set; }

            public virtual bool IsExcluded { get; set; }

            public virtual string ToString(string separator)
            {
                return string.Join(separator, Values);
            }

            public override string ToString()
            {
                if (Values.Any())
                    return string.Join(",", Values);
                else
                    return Name;
            }
        }

        public class CommentLine : Line 
        {
            public CommentLine()
            {
            }

            public CommentLine(string content)
            {
                Content = content;
            }

            public override bool IsExcluded => true;

            public string Content { get; set; }

            public override string ToString()
            {
                return Content;
            }
        }

        #endregion

        #region cell class

        public class Cell
        {
            public string Value { get; set; }

            public bool IsExcluded { get; set; }

            public ExclusionType? DirectionExclusionType { get; set; }
        }

        #endregion

        #region types

        public enum ExclusionType
        {
            Horizontal,
            Vertical
        }

        [Flags]
        public enum LineType
        {
            Data = 0,
            Empty = 1,
            Excluded = 2,
            NonFunctional = Empty | Excluded,
            
            TableName = 4,
            ColumnsNames = 8,
            DefaultValues = 16,
            Custom = 32,
            Header = TableName | ColumnsNames | DefaultValues | Custom,
        }

        #endregion
    }
}
