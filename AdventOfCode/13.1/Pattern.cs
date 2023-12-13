using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13._1
{
    internal class Pattern
    {
        private string[] pattern;

        public Pattern(string[] patternLines)
        {
            this.pattern = patternLines;
        }

        public int GetVerticalMirrorIndex()
        {
            int mirrorIndex = 0;
            string[] transposedPattern = TransposePattern(pattern);
            while (mirrorIndex != -1 && mirrorIndex < transposedPattern.Length)
            {
                int newMirrorIndex = GetIndexOfDuplicatedRows(transposedPattern, mirrorIndex);
                if (newMirrorIndex != -1 && AreMirrorImages(transposedPattern, newMirrorIndex))
                    return newMirrorIndex;
                mirrorIndex++;
            }
            return -1;
        }

        private string[] TransposePattern(string[] patternLines)
        {
            int newRows = patternLines[0].Length;
            int newColumns = patternLines.Length;

            string[] transposedPatternLines = new string[newRows];

            for (int i = 0; i < newRows; i++)
                transposedPatternLines[i] = new string(patternLines.Select(line => line[i]).ToArray());
            return transposedPatternLines;
        }


        public int GetHorizontalMirrorIndex()
        {
            int mirrorIndex = 0;
            while (mirrorIndex != -1 && mirrorIndex < pattern.Length)
            {
                int newMirrorIndex = GetIndexOfDuplicatedRows(pattern, mirrorIndex);
                if (newMirrorIndex != -1 && AreMirrorImages(pattern, newMirrorIndex))
                    return newMirrorIndex;
                mirrorIndex++;
            }
            return -1;
        }

        private bool AreMirrorImages(string[] pattern, int mirrorIndex)
        {
            int mirroredIndex = mirrorIndex + 1;
            while (mirroredIndex < pattern.Length && mirrorIndex >= 0)
            {
                if (pattern[mirrorIndex] != pattern[mirroredIndex])
                    return false;
                mirrorIndex--;
                mirroredIndex++;
            }
            return true;
        }

        private int GetIndexOfDuplicatedRows(string[] pattern, int startingIndex = 0)
        {
            for (int i = startingIndex; i < pattern.Length - 1; i++)
            {
                if (pattern[i] == pattern[i + 1])
                    return i;
            }
            return -1;
        }


    }
}
