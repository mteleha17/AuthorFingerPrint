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
    public class TextStore : IItemStore<Text, ITextModel>
    {
        private FingerprintV2Entities _db;
        private IModelFactory _modelFactory;

        public TextStore(FingerprintV2Entities db, IModelFactory modelFactory)
        {
            _db = db;
            _modelFactory = modelFactory;
        }

        public void Add(ITextModel model)
        {
            if (_db.Texts.Any(x => x.Name == model.GetName()))
            {
                throw new ArgumentException($"Cannot add model since a model already exists in the database with name {model.GetName()}.");
            }

        }

        public void Delete(ITextModel model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Text, bool>> criteria)
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

        private Count TranslateCounts(ISingleWordCountModel model)
        {
            if (model.GetLength() != 13)
            {
                throw new ArgumentException("Please pass a model with length 13.");
            }
            Count output = new Count()
            {
                one = model.GetAt(0),
                two = model.GetAt(1),
                three = model.GetAt(2),
                four = model.GetAt(3),
                five = model.GetAt(4),
                six = model.GetAt(5),
                seven = model.GetAt(6),
                eight = model.GetAt(7),
                nine = model.GetAt(8),
                ten = model.GetAt(9),
                eleven = model.GetAt(10),
                twelve = model.GetAt(11),
                thirteen = model.GetAt(12)
            };
            return output;
        }

        private ISingleWordCountModel TranslateCounts(Count count)
        {
            throw new NotImplementedException();
            //ISingleWordCountModel output = _modelFactory.GetSingleCountModel(13);
            //output.SetAt(0, count.one);

        }
    }
}
