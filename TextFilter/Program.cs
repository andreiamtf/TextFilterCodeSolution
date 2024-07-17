using TextFilter.Filters;
using TextFilter.Services.FileReader;
using TextFilter.Services.FilterManager;
using TextFilter.Services.TextProcessor;

namespace TextFilter;

public class Program
{
    static void Main()
    {
        var filePath = @"C:\02 Projects\TextFilter\TextFilter\TextSamples\TextSampleOne.txt"; // Update file path

        IFileReaderService fileReader = new FileReaderService();
   
        var filters = new List<IFilter>
        {
            new MidCharacterExclusionFilter('a', 'e', 'i', 'o', 'u'), // Exclude words with vowels in the middle
            new MinimumLengthFilter(3), // Exclude words shorter than 3 characters
            new CharacterExclusionFilter('t', 'r') // Exclude words containing 't'
        };

        IFilterManagerService filterService = new FilterManagerService(filters);
        ITextProcessorService textProcessor = new TextProcessorService(filterService);

        var inputText = fileReader.ReadFile(filePath);

        Console.WriteLine("Original Text:");
        Console.WriteLine(inputText);

        var processedText = textProcessor.ProcessText(inputText);

        Console.WriteLine("Processed Text:");
        Console.WriteLine(processedText);
    }
}