namespace TextFilter.Filters
{
    public class MinimumLengthFilter : IFilter
    {
        private readonly int _minLength;

        public MinimumLengthFilter(int minLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength), "Minimum length must be greater than zero.");
            }

            _minLength = minLength;
        }

        public bool ApplyFilter(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            return word.Length < _minLength;
        }
    }
}