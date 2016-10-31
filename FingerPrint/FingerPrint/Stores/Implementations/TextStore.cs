using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint;
using FingerPrint.AuxiliaryClasses;

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
            if (model.GetLength() != UniversalCountSize.CountSize)
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
            ISingleWordCountModel output = _modelFactory.GetSingleCountModel(UniversalCountSize.CountSize);
            output.SetAt(0, count.one);
            output.SetAt(1, count.two);
            output.SetAt(2, count.three);
            output.SetAt(3, count.four);
            output.SetAt(4, count.five);
            output.SetAt(5, count.six);
            output.SetAt(6, count.seven);
            output.SetAt(7, count.eight);
            output.SetAt(8, count.nine);
            output.SetAt(9, count.ten);
            output.SetAt(10, count.eleven);
            output.SetAt(11, count.twelve);
            output.SetAt(12, count.thirteen);
            return output;
        }
    }
}
