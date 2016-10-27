using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint;

namespace FingerPrint.Stores
{
    public class TextStore : ITextStore
    {
        private FingerprintV2Entities db;

        public TextStore()
        {
            db = new FingerprintV2Entities();
        }

        public void Add(ITextModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(ITextModel model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Text, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public ISingleWordCountModel GetCountsWithoutQuotes()
        {
            throw new NotImplementedException();
        }

        public ISingleWordCountModel GetCountsWithQuotes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITextModel> GetMany(Expression<Func<Text, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public ITextModel GetOne(Expression<Func<Text, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public void Modify(ITextModel model)
        {
            throw new NotImplementedException();
        }
    }
}
