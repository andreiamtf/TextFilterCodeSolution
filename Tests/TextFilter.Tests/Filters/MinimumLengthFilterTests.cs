using NUnit.Framework;
using TextFilter.Filters;

namespace TextFilter.Tests.Filters
{
    [TestFixture]
    public class MinimumLengthFilterTests
    {
        [Test]
        public void GivenAWordLengthEqualsMinLength_WhenApplyFilterIsCalled_ThenReturnsTrue()
        {
            var filter = new MinimumLengthFilter(3);
            var word = "cat";

            var result = filter.ApplyFilter(word);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenAWordLengthLessThanMinLength_WhenApplyFilterIsCalled_ThenReturnsTrue()
        {
            var filter = new MinimumLengthFilter(5);
            var word = "it";

            var result = filter.ApplyFilter(word);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenANegativeMinLength_ThenThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MinimumLengthFilter(-1));
        }

        [Test]
        public void GivenANullWord_WhenApplyFilterIsCalled_ThrowsArgumentNullException()
        {
            var filter = new MinimumLengthFilter(3);

            Assert.Throws<ArgumentNullException>(() => filter.ApplyFilter(null));
        }
    }
}