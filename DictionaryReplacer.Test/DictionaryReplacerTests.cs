using NUnit.Framework;
using DictionaryReplacer;
using System.Collections.Generic;

namespace DictionaryReplacer.Test
{
    public class DictionaryReplacerTests
    {
        private DictionaryReplacer _dictionaryReplacer;
        [SetUp]
        public void Setup()
        {
            _dictionaryReplacer = new DictionaryReplacer();
        }

        [Test]
        public void GivenAWordPrefixedWithADollarSigns_WhenExtractingKeys_ShouldReturnKeys()
        {          
                // Arrange
                string expected = "name";

                // Act
                string result = _dictionaryReplacer.TrimDollarSigns("$name$");

                // Assert
                Assert.AreEqual(expected, result);            
        }
        [Test]
        public void GivenAStringWithWordPrefixedWithADollarSign_WhenExtractingKeys_ShouldReturnAListOfKeys()
        {
            // Arrange
            List<string> expected= new List<string>() { "temp","name"};
            string[] splitwords = { "$temp$", "here comes the name", "$name$" };

            // Act
            List <string> result = _dictionaryReplacer.GetKeys(splitwords);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenADictionaryAndAListOfKeys_WhenFindingValuesMatchingKeys_ShouldReturnAListOfValues()
        {
            // Arrange
            List<string> expected = new List<string>() { "temporary", "John Doe" };
            List<string> keys = new List<string>() { "temp","name"};
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" },{ "name", "John Doe" } };

            // Act
            List<string> result = _dictionaryReplacer.FindValuesInDictionary(dictionary,keys);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenAnArrayWithTheOriginalStringBrokenUpAndAlistOfValues_WhenReplacingKeysWithMatchingValues_ShouldReturnAstringWithTheWordsReplaced()
        {
            // Arrange
            string expected = "temporary here comes the name John Doe";
            string[] wordList = { "$temp$","here comes the name","$name$" };
            List<string> valuesFound = new List<string>() { "temporary", "John Doe" };

            // Act
            string result = _dictionaryReplacer.ReplaceDictionary(wordList, valuesFound);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenAStringWithOneWordAndADictionsrt_WhenReplacingKeysWithMatchingValues_ShouldReturnAstringWithTheWordsReplaced()
        {
            // Arrange
            string expected = "temporary";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" } };
            string sentence = "$temp$";

            // Act
            string result = _dictionaryReplacer.Replace(sentence,dictionary);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenAStringAndADictionary_WhenReplacingKeysWithMatchingValues_ShouldReturnAstringWithTheWordsReplaced()
        {
            // Arrange
            string expected = "temporary here comes the name John Doe";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" },{"name", "John Doe" } };
            string sentence = "$temp$ here comes the name $name$";

            // Act
            string result = _dictionaryReplacer.Replace(sentence, dictionary);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenAnEmptyStringAndADictionary_WhenReplacingKeysWithMatchingValues_ShouldReturnAstringWithTheWordsReplaced()
        {
            // Arrange
            string expected = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() {  };
            string sentence = "";

            // Act
            string result = _dictionaryReplacer.Replace(sentence, dictionary);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenTemplateString_WhenReplacingKeysWithMultipleWords_ShouldReturnAstringWithTheWordsReplaced()
        {
            // Arrange
            string expected = "This should work neatly";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "will it work", "work neatly" } };
            string sentence = "This should $will it work$";

            // Act
            string result = _dictionaryReplacer.Replace(sentence, dictionary);

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}