﻿using System.Collections.Generic;

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

        private static IEnumerable<string> AllTags(string sequence, int length)
        {
            for(int i = 0; i < sequence.Length - length; i++)
            {
                yield return sequence.Substring(i, length);
            }
        }
    }
}
