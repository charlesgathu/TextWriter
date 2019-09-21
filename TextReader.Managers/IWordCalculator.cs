namespace TextReader.Managers
{
    public interface IWordCalculator
    {

        int CountCharacters(char character, string content);
        int CountWords(string content);

    }
}