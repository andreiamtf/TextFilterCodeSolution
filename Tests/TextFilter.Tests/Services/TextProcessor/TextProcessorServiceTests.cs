using NUnit.Framework;
using Moq;
using TextFilter.Services.FilterManager;
using TextFilter.Services.TextProcessor;

namespace TextFilter.Tests.Services.TextProcessor
{
    [TestFixture]
    public class TextProcessorServiceTests
    {
        [Test]
        public void GivenTheProvidedTextIsNotNull_WhenProcessTextIsCalled_ThenTheCorrectFilteredTextIsReturned()
        {
            var mockFilterService = new Mock<IFilterManagerService>();

            var expectedText = new List<string> { "filtered", "words" };
            mockFilterService.Setup(fs => fs.ApplyFilters(
                    It.IsAny<string[]>()))
                .Returns(expectedText);

            var textProcessor = new TextProcessorService(mockFilterService.Object);

            const string inputText = "Mock test input";
            var result = textProcessor.ProcessText(inputText);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("filtered words"));
        }

        [Test]
        public void GivenTheProvidedTextIsNull_WhenProcessTextIsCalled_ThenAnArgumentNullExceptionIsThrown()
        {
            var textProcessor = new TextProcessorService(Mock.Of<IFilterManagerService>());

            Assert.Throws<ArgumentNullException>(() => textProcessor.ProcessText(null));
        }

        [Test]
        public void GivenEmptyText_WhenProcessTextIsCalled_ThenEmptyStringIsReturned()
        {
            var textProcessor = new TextProcessorService(Mock.Of<IFilterManagerService>());

            var result = textProcessor.ProcessText("");

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GivenDifferentFilterResults_WhenProcessTextIsCalled_ThenCorrectFilteredTextIsReturned()
        {
            var mockFilterService = new Mock<IFilterManagerService>();

            mockFilterService.SetupSequence(fs => fs.ApplyFilters(It.IsAny<string[]>()))
                .Returns(new List<string> { "filtered", "words" }) 
                .Returns(new List<string> { "different", "filters" });

            var textProcessor = new TextProcessorService(mockFilterService.Object);

            const string inputText = "Mock test input";

            // First call
            var firstCallResult = textProcessor.ProcessText(inputText);
            Assert.That(firstCallResult, Is.EqualTo("filtered words"));

            // Second call
            var secondCallResult = textProcessor.ProcessText(inputText);
            Assert.That(secondCallResult, Is.EqualTo("different filters"));
        }
    }
}