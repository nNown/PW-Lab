using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW_Lab5
{
    public static class PatternFinder
    {
        public static Dictionary<string, int> FindPatterns(string sequence, int length)
        {
            var patterns = new Dictionary<string, int>();

            foreach(var tag in AllTags(sequence, length))
            {
                if(!patterns.ContainsKey(tag))
                {
                    patterns.Add(tag, 0);
                }

                patterns[tag] += 1;
            }

            return patterns;
        }

        private static string Tag(string sequence, int start, int end)
        {
            var tag = string.Empty;
            for(int i = start; i < end; i++)
            {
                tag += sequence[i];
            }
            return tag;
        }

        private static IEnumerable<string> AllTags(string sequence, int length)
        {
            for(int i = 0; i < sequence.Length - length; i++)
            {
                yield return Tag(sequence, i, i + length);
            }
        }
    }
}
