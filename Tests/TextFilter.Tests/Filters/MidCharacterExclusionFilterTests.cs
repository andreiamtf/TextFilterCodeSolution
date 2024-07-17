using NUnit.Framework;
using TextFilter.Filters;

namespace TextFilter.Tests.Filters
{
    [TestFixture]
    public class MidCharacterExclusionFilterTests
    {
        private MidCharacterExclusionFilter _exclusionFilter;

        [SetUp]
        public void Setup()
        {
            _exclusionFilter = new MidCharacterExclusionFilter('a', 'e', 'i', 'o', 'u');
        }

        [TestCase("clean", true)]
        [TestCase("what", true)]
        [TestCase("Alice", true)]
        public void GivenWordsWithVowelsInCentre1_WhenApplyFiltersIsCalled_ThenReturnsTrue(string word, bool isFilteredApplied)
        {
            var result = _exclusionFilter.ApplyFilter(word);

            Assert.That(result, Is.EqualTo(isFilteredApplied));
        }

        [Test]
        public void GivenWordsWithVowelsInCentre2_WhenApplyFiltersIsCalled_ThenReturnsTr()
        {
            var word = "currently";

            var result = _exclusionFilter.ApplyFilter(word);

            Assert.That(result, Is.True);
        }

        [TestCase("the", false)]
        [TestCase("rather", false)]
        public void GivenWordsWithoutVowelsInMiddle_WhenApplyFiltersIsCalled_ThenReturnsFalse(string word, bool isFilteredApplied)
        {
            var result = _exclusionFilter.ApplyFilter(word);

            Assert.That(result, Is.EqualTo(isFilteredApplied));
        }

        [Test]
        public void GivenShortWords_WhenApplyFiltersIsCalled_ThenReturnsFalse()
        {
            var word = "it";

            var result = _exclusionFilter.ApplyFilter(word);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenNullWords_WhenApplyFiltersIsCalled_ThenAnArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _exclusionFilter.ApplyFilter(null));
        }

        [Test]
        public void GivenNullCharactersToFilterBy_ThenAnArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new MidCharacterExclusionFilter(null));
        }
    }
}
