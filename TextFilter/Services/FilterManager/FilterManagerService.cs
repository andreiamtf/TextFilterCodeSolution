using TextFilter.Filters;

namespace TextFilter.Services.FilterManager
{
    public class FilterManagerService : IFilterManagerService
    {
        private readonly IEnumerable<IFilter> _filters;

        public FilterManagerService(IEnumerable<IFilter> filters)
        {
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> words)
        {
            ArgumentNullException.ThrowIfNull(words);

            return GetFilteredWords(words);
        }

        private IEnumerable<string> GetFilteredWords(IEnumerable<string> words)
        {
            var filteredWords = new List<string>();

            foreach (var word in words)
            {
                var passedAllFilters = _filters.All(filter => !filter.ApplyFilter(word));

                if (passedAllFilters)
                {
                    filteredWords.Add(word);
                }
            }

            return filteredWords;
        }
    }
}
