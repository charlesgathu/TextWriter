using System.Collections.Generic;
using System.Threading.Tasks;

namespace TextReader.Managers
{
    public interface ITextReaderManager
    {

        string Sort(string content, SortOption option);

        TextStatistics GetStatistics(string content);

    }
}