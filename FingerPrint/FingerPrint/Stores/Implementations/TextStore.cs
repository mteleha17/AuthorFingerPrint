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
    public class TextStore : ITextStore<ISingleWordCountModel>
    {
        private FingerprintEntities1 db;

        public TextStore()
        {
            db = new FingerprintEntities1();
        }

        public void Add(ITextModel<ISingleWordCountModel> model)
        {
            if (Exists(x => x.Name == model.GetName()))
            {
                throw new ArgumentException($"Cannot add text because another text already has the name {model.GetName()}");
            }
            ISingleWordCountModel withQuotes = model.GetCountsWithQuotes();
            NumbersWithQuote numbersWithQuotes = TranslateCountsWithQuotes(withQuotes);
            db.NumbersWithQuotes.Add(numbersWithQuotes);
            ISingleWordCountModel withoutQuotes = model.GetCountsWithoutQuotes();
            NumbersWithoutQuote numbersWithoutQuotes = TranslateCountsWithoutQuotes(withoutQuotes);
            db.NumbersWithoutQuotes.Add(numbersWithoutQuotes);
            //File file = new File()
            //{
            //    Name = model.GetName(),
            //    Author = model.GetAuthor(),
            //    NumbersWithQuotes = numbersWithQuotes,

            //};
        }

        public void Delete(ITextModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<File, bool>> criteria)
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

        public IEnumerable<ITextModel<ISingleWordCountModel>> GetMany(Expression<Func<File, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public ITextModel<ISingleWordCountModel> GetOne(Expression<Func<File, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public void Modify(ITextModel<ISingleWordCountModel> model)
        {
            throw new NotImplementedException();
        }

        private NumbersWithQuote TranslateCountsWithQuotes(ISingleWordCountModel model)
        {
            var output = new NumbersWithQuote();
            List<Action<int>> actions = new List<Action<int>>()
            {
                i => output.one = model.GetAt(i),
                i => output.two = model.GetAt(i),
                i => output.three = model.GetAt(i),
                i => output.four = model.GetAt(i),
                i => output.five = model.GetAt(i),
                i => output.six = model.GetAt(i),
                i => output.seven = model.GetAt(i),
                i => output.eight = model.GetAt(i),
                i => output.nine = model.GetAt(i),
                i => output.ten = model.GetAt(i),
                i => output.eleven = model.GetAt(i),
                i => output.tweleve = model.GetAt(i),
                i => output.thirteen = model.GetAt(i),
                i => output.fourteen = model.GetAt(i),
                i => output.fifteen = model.GetAt(i),
                i => output.sixteen = model.GetAt(i),
                i => output.seventeen = model.GetAt(i),
                i => output.eighteen = model.GetAt(i),
                i => output.nineteen = model.GetAt(i),
                i => output.twentyPlus = model.GetAt(i)
            };
            for (int x = 0; x < (model.GetLength()); x++)
            {
                actions[x](x);
            }
            return output;
        }

        private NumbersWithoutQuote TranslateCountsWithoutQuotes(ISingleWordCountModel model)
        {
            var output = new NumbersWithoutQuote();
            List<Action<int>> actions = new List<Action<int>>()
            {
                i => output.one = model.GetAt(i),
                i => output.two = model.GetAt(i),
                i => output.three = model.GetAt(i),
                i => output.four = model.GetAt(i),
                i => output.five = model.GetAt(i),
                i => output.six = model.GetAt(i),
                i => output.seven = model.GetAt(i),
                i => output.eight = model.GetAt(i),
                i => output.nine = model.GetAt(i),
                i => output.ten = model.GetAt(i),
                i => output.eleven = model.GetAt(i),
                i => output.twelve = model.GetAt(i),
                i => output.thirteen = model.GetAt(i),
                i => output.fourteen = model.GetAt(i),
                i => output.fifteen = model.GetAt(i),
                i => output.sixteen = model.GetAt(i),
                i => output.seventeen = model.GetAt(i),
                i => output.eighteen = model.GetAt(i),
                i => output.nineteen = model.GetAt(i),
                i => output.twentyPlus = model.GetAt(i)
            };
            for (int x = 0; x < (model.GetLength()); x++)
            {
                actions[x](x);
            }
            return output;
        }
    }
}
