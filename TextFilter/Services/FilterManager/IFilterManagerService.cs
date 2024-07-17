namespace TextFilter.Services.FilterManager;

public interface IFilterManagerService
{
    IEnumerable<string> ApplyFilters(IEnumerable<string> words);
}