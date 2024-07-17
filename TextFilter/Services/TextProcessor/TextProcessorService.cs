using System.Text;
using TextFilter.Services.FilterManager;

namespace TextFilter.Services.TextProcessor;

public class TextProcessorService : ITextProcessorService
{
    private readonly IFilterManagerService _filterService;
    private static readonly char[] Separator = [' ', '\n', '\r', ',', '.', '!', '?', '-', '_', ';', ':'];

    public TextProcessorService(IFilterManagerService filterService)
    {
        _filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
    }

    public string ProcessText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var words = text.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        var filteredWords = _filterService.ApplyFilters(words);
        return BuildFilteredText(filteredWords);
    }

    private static string BuildFilteredText(IEnumerable<string> filteredWords)
    {
        var resultBuilder = new StringBuilder();
        foreach (var word in filteredWords)
        {
            if (resultBuilder.Length > 0)
            {
                resultBuilder.Append(' ');
            }
            resultBuilder.Append(word);
        }

        return resultBuilder.ToString();
    }
}

