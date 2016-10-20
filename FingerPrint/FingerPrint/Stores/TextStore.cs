using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Stores
{
    public class TextStore : ITextStore
    {
        public void Add(File entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(File entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<File> Get(Expression<Func<File, bool>> test)
        {
            throw new NotImplementedException();
        }

        public NumbersWithoutQuote GetCountsWithoutQuotes()
        {
            throw new NotImplementedException();
        }

        public NumbersWithQuote GetCountsWithQuotes()
        {
            throw new NotImplementedException();
        }

        public void Modify(File entity)
        {
            throw new NotImplementedException();
        }
    }
}
