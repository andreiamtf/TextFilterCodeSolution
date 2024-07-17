using Moq;
using NUnit.Framework;
using TextFilter.Filters;
using TextFilter.Services.FilterManager;

namespace TextFilter.Tests.Services.FilterManager
{
    [TestFixture]
    public class FilterManagerServiceTests
    {
        [Test]
        public void GivenTheProvidedFiltersIsNull_WhenApplyFiltersIsCalled_ThenAnArgumentNullExceptionIsThrown()
        {
            var filters = new List<IFilter>();
            var filterManagerService = new FilterManagerService(filters);

            Assert.Throws<ArgumentNullException>(() =>
            {
                filterManagerService.ApplyFilters(null);
            });
        }

        [Test]
        public void GivenTheProvidedFiltersIsEmpty_WhenApplyFiltersIsCalled_ThenAnEmptyCollectionIsReturned()
        {
            var filters = new List<IFilter>();
            var filterManagerService = new FilterManagerService(filters);

            var result = filterManagerService.ApplyFilters(Array.Empty<string>());

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenNoFilters_WhenApplyFiltersIsCalled_ThenAllWordsAreReturned()
        {
            var filters = new List<IFilter>();
            var filterManagerService = new FilterManagerService(filters);
            var words = new[] { "morning", "afternoon", "night" };

            var result = filterManagerService.ApplyFilters(words);

            Assert.That(result, Is.EquivalentTo(words));
        }

        [Test]
        public void GivenFiltersAreProvided_WhenApplyFiltersIsCalled_ThenFilteredWordsAreReturned()
        {
            var mockFilter = new Mock<IFilter>();
            mockFilter.Setup(f => f.ApplyFilter(It.IsAny<string>()))
                .Returns<string>(word => word.StartsWith("a"));

            var filters = new List<IFilter> { mockFilter.Object};
            var filterManagerService = new FilterManagerService(filters);

            var words = new[] { "morning", "afternoon", "night", "longMorning", "longAfternoon" };

            var result = filterManagerService.ApplyFilters(words);

            Assert.That(result, Is.EquivalentTo(new[] { "morning", "night", "longMorning", "longAfternoon" }));
        }
    }
}

