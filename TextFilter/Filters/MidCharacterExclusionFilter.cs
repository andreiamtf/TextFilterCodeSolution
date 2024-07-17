namespace TextFilter.Filters
{
    public class MidCharacterExclusionFilter : IFilter
    {
        private readonly char[] _charactersToCheck;
        private readonly int _minimumLengthFilter = 3;

        public MidCharacterExclusionFilter(params char[] charactersToCheck)
        {
            _charactersToCheck = charactersToCheck ?? throw new ArgumentNullException(nameof(charactersToCheck));
        }

        public bool ApplyFilter(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            if (word.Length <= _minimumLengthFilter)
                return false; // Short words do not get filtered

            var middleIndex = word.Length / 2;

            for (var i = middleIndex - 1; i <= middleIndex; i++)
            {
                if (i >= 0 && i < word.Length && _charactersToCheck.Contains(char.ToLower(word[i])))
                {
                    return true; // Filter out words with characters to check in the middle 1 or 2 characters
                }
            }

            return false; // Word passes the filter if no characters to check found in middle 1 or 2 characters
        }
    }
}
