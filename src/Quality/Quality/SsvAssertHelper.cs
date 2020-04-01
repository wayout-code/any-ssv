namespace Wayout.AnySsv.Quality
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class SsvAssertHelper
    {
        public static void AssertSsvEqualsContentLines(Ssv ssv, IEnumerable<string> contentLines)
        {
            for (int i = 0; i < ssv.Lines.Count; i++)
            {
                var contentLine = contentLines.ElementAt(i);
                for (int j = 0; j < ssv.Lines[i].Values.Count; j++)
                {
                    var value = ssv.Lines[i].Values[j];
                    
                    Assert.IsTrue(contentLine.Contains(value));
                }
            }
        }
    }
}
