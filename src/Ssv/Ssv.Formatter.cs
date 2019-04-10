namespace Wayout.Ssv
{
    using System;
    using System.Text;

    public class SsvFormatter
    {
        public SsvFormatter()
        {
            SsvNotation = new Notation();
        }

        public Notation SsvNotation { get; set; }

        public string Format(Ssv ssv)
        {
            var sb = new StringBuilder();

            foreach (var line in ssv.Lines)
            {
                switch (line.LineType)
                {
                    case Ssv.LineType.Data:
                        sb.AppendLine(line.ToString(SsvNotation.ValueDelimiter));
                        continue;

                    case Ssv.LineType.Comment:
                        if ((line as Ssv.CommentLine)?.Content != null)
                            sb.Append(SsvNotation.LineExclusionMark)
                                .AppendLine((line as Ssv.CommentLine).Content);
                        else
                            sb.AppendLine();
                        continue;

                    case Ssv.LineType.TableName:
                        sb.Append(SsvNotation.TableStartMark)
                            .Append(line.Name)
                            .AppendLine(SsvNotation.TableEndMak);
                        continue;

                    case Ssv.LineType.HeaderColumns:
                    case Ssv.LineType.Header:
                        sb.Append(SsvNotation.HeaderStartMark)
                            .Append(line.ToString(SsvNotation.ValueDelimiter))
                            .AppendLine(SsvNotation.HeaderEndMark);
                        continue;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            sb.Remove(sb.Length - SsvNotation.LineDelimiter.Length, SsvNotation.LineDelimiter.Length);

            return sb.ToString();
        }
    }
}
