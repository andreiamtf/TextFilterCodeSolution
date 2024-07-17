namespace TextFilter.Filters;

public class CharacterExclusionFilter : IFilter
{
    private readonly HashSet<char> _excludedLettersSet;

    public CharacterExclusionFilter(params char[] excludedLetters)
    {
        if (excludedLetters == null)
        {
            throw new ArgumentNullException(nameof(excludedLetters));
        }
      
        _excludedLettersSet = new HashSet<char>(excludedLetters);
    }

    public bool ApplyFilter(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word));
        }
           
        var lowerCaseWord = word.ToLower();

        foreach (var letter in lowerCaseWord)
        {
            if (_excludedLettersSet.Contains(letter))
            {
                return true;
            }
        }
        return false;
    }
}