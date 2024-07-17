using NUnit.Framework;
using TextFilter.Filters;

namespace TextFilter.Tests.Filters
{
    [TestFixture]
    public class CharacterExclusionFilterTests
    {
        [Test]
        public void GivenExcludesSingleLetter_WhenApplyFiltersIsCalled_ThenReturnsTrue()
        {
            var filter = new CharacterExclusionFilter('m');

            var result = filter.ApplyFilter("morning");

            Assert.That(result, Is.True);
        }

        [TestCase("morning", false)]
        [TestCase("afternoon", true)]
        [TestCase("night", false)]
        [TestCase("longMorning", false)]
        public void GivenExcludesMultipleLetters_WhenApplyFiltersIsCalled_ThenReturnsTrue(string word, bool isFilteredApplied)
        {
            var filter = new CharacterExclusionFilter('a', 'e', 'u');

            var result1 = filter.ApplyFilter(word);

            Assert.That(result1, Is.EqualTo(isFilteredApplied));
        }

        [Test]
        public void GivenNoExcludedLetters_WhenApplyFiltersIsCalled_ReturnsFalse()
        {
            var filter = new CharacterExclusionFilter();

            var result = filter.ApplyFilter("morning");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GiveNoTextIsProvided_WhenApplyFiltersIsCalled_ThenReturnsFalse() 
        {
            var filter = new CharacterExclusionFilter('a');

            Assert.Throws<ArgumentNullException>(() => filter.ApplyFilter(null));
        }

        [Test]
        public void GivenNullExcludedLetters_ThenAnArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CharacterExclusionFilter(null));
        }
    }
}
