using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Db
{
    public interface IDataStore
    {
        Task<IEnumerable<Question>> AllQuestionAnswer();
    }
}
