using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextReader.Managers
{
    public class WordCalculator : IWordCalculator
    {

        public int CountCharacters(char character, string content)
        {
            return content.Sum(sm => (sm == character) ? 1 : 0);
        }

        public int CountWords(string content)
        {
            return GetWords(content).Count();
        }

        private IEnumerable<string> GetWords(string content)
        {
            foreach (var word in content.Split(' ', '\r', '\n'))
            {
                if (word.ToCharArray().Any(c => char.IsLetterOrDigit(c)))
                {
                    yield return word;
                }
            }
        }
    }
}
