using Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Db
{
    public class DataStore : IDataStore
    {
        public async Task<IEnumerable<Question>> AllQuestionAnswer() =>
            await Task.Run(() => Data.AllQuestionAnswers.ToList());
    }
}
