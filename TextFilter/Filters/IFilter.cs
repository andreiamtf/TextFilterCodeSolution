namespace TextFilter.Filters
{
    public interface IFilter
    {
        bool ApplyFilter(string word);
    }
}
