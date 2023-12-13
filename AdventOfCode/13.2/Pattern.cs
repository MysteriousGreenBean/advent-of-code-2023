namespace _13._2
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
                (int newMirrorIndex, int changedCharacterIndex) = GetIndexOfDuplicatedRows(transposedPattern, mirrorIndex);
                if (newMirrorIndex != -1 && AreMirrorImages(transposedPattern, newMirrorIndex, ref changedCharacterIndex) && changedCharacterIndex != -1)
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
                (int newMirrorIndex, int changedCharacterIndex) = GetIndexOfDuplicatedRows(pattern, mirrorIndex);
                if (newMirrorIndex != -1 && AreMirrorImages(pattern, newMirrorIndex, ref changedCharacterIndex) && changedCharacterIndex != -1)
                    return newMirrorIndex;
                mirrorIndex++;
            }
            return -1;
        }

        private bool AreMirrorImages(string[] pattern, int mirrorIndex, ref int changedCharacterIndex)
        {  
            int mirroredIndex = mirrorIndex + 1;
            mirrorIndex--;
            mirroredIndex++;
            while (mirroredIndex < pattern.Length && mirrorIndex >= 0)
            {
                if (changedCharacterIndex != -1)
                {
                    if (pattern[mirrorIndex] != pattern[mirroredIndex])
                        return false;
                }
                else
                {
                    int? index = AreEqualEnough(pattern[mirrorIndex], pattern[mirroredIndex]);
                    if (index.HasValue && index.Value != -1)
                    {
                        changedCharacterIndex = index.Value;
                    }
                    else if (!index.HasValue)
                        return false;
                }
                mirrorIndex--;
                mirroredIndex++;
            }
            return true;
        }

        private (int duplicatedRowIndex, int characterDiffIndex) GetIndexOfDuplicatedRows(string[] pattern, int startingIndex = 0)
        {
            for (int i = startingIndex; i < pattern.Length - 1; i++)
            {
                int? index = AreEqualEnough(pattern[i], pattern[i + 1]);
                if (index.HasValue)
                    return (i, index.Value);
            }
            return (-1, -1);
        }

        /// If strings differ by one character, return the index of the character that differs. 
        /// If they are equal - return -1. <summary>
        /// If strings differ by more than one character - return null.
        private int? AreEqualEnough(string string1, string string2)
        {
            int index = -1;
            for (int i = 0; i < string1.Length; i++)
            {
                if (string1[i] != string2[i])
                {
                    if (index != -1)
                        return null;
                    index = i;
                }
            }
            return index;
        }
    }
}
