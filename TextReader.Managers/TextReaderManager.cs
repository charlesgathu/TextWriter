using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextReader.Managers
{
    public class TextReaderManager : ITextReaderManager
    {

        private readonly IWordCalculator calculator;

        public TextReaderManager(IWordCalculator calculator)
        {
            this.calculator = calculator;
        }

        public TextStatistics GetStatistics(string content)
        {
            return new TextStatistics
            {
                Hyphens = calculator.CountCharacters('-', content),
                Spaces = calculator.CountCharacters(' ', content),
                Words = calculator.CountWords(content)
            };
        }

        public string Sort(string content, SortOption option)
        {
            var paragraphs = content.Split('\n');
            IOrderedEnumerable<string> orderedParagraphs;

            switch (option)
            {
                case SortOption.Ascending:
                    orderedParagraphs = paragraphs.OrderBy(o => o);
                    break;
                case SortOption.Descending:
                    orderedParagraphs = paragraphs.OrderByDescending(o => o);
                    break;
                case SortOption.None:
                default:
                    return content;
            }

            return string.Join("\n", orderedParagraphs);
        }
    }
}
