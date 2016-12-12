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
    public class TextStore : ITextStore
    {
        private FingerprintLite13Entities _db;
        private IModelFactory _modelFactory;

        public TextStore(FingerprintLite13Entities db, IModelFactory modelFactory)
        {
            _db = db;
            _modelFactory = modelFactory;
        }

        public void Add(ITextModel model)
        {
            string name = model.GetName();
            if (_db.Texts.Any(x => x.Name == name))
            {
                throw new ArgumentException($"Cannot add model since a model already exists in the database with name {model.GetName()}.");
            }
            WordCount withQuotes = TranslateCounts(model.GetCountsWithQuotes());
            _db.WordCounts.Add(withQuotes);
            _db.SaveChanges();
            WordCount withoutQuotes = TranslateCounts(model.GetCountsWithoutQuotes());
            
            _db.WordCounts.Add(withoutQuotes);
            _db.SaveChanges();

            Text text = new Text()
            {
                Name = model.GetName(),
                Author = model.GetAuthor(),
                WithQuotesId = withQuotes.Id,
                WithoutQuotesId = withoutQuotes.Id,
            };
            _db.Texts.Add(text);
            _db.SaveChanges();
        }

        public void Delete(ITextModel model)
        {
            string name = model.GetName();
            Text text = _db.Texts.FirstOrDefault(x => x.Name == name);
            if (text == null)
            {
                throw new ArgumentException($"Cannot delete text {model.GetName()} since it does not exist in the database.");
            }
            if (_db.Text_Grouping.Any(x => x.TextId == text.Id))
            {
                throw new ArgumentException($"Cannot delete text {model.GetName()} because it is currently a member of a group.");
            }
            //if (_db.Groupings.Any(x => x.Texts.Contains(text)))
            //{
            //    throw new ArgumentException($"Cannot delete text {model.GetName()} because it is currently a member of a group.");
            //}
            _db.Texts.Remove(text);
            _db.SaveChanges();
        }

        public bool Exists(Expression<Func<Text, bool>> criteria)
        {
            return _db.Texts.Any(criteria);
        }

        public IEnumerable<ITextModel> GetMany(Expression<Func<Text, bool>> criteria)
        {
            foreach (Text text in _db.Texts.Where(criteria))
            {
                yield return GetOne(x => x.Id == text.Id);
            }
            //for (int i = 0; i < 1; i++)
            //{
            //    yield return null;
            //}
        }

        public ITextModel GetOne(Expression<Func<Text, bool>> criteria)
        {
            Text text = _db.Texts.FirstOrDefault(criteria);
            if (text == null)
            {
                return null;
            }
            IFlexibleWordCountModel counts = GetCountsFromText(text);
            ITextModel model = _modelFactory.GetTextModel(text.Name, counts);
            model.SetAuthor(text.Author);
            model.SetIncludeQuotes(Convert.ToBoolean(text.IncludeQuotes));
            return model;
        }

        public void ModifyAuthor(ITextModel model, string newAuthor)
        {
            string name = model.GetName();
            Text text = _db.Texts.FirstOrDefault(x => x.Name == name);
            if (text == null)
            {
                throw new ArgumentException("Cannot update model since no corresponding text exists in the database.");
            }
            if (string.IsNullOrWhiteSpace(newAuthor))
            {
                text.Author = null;
            }
            else
            {
                text.Author = newAuthor;
            }
            _db.SaveChanges();
        }

        public void ModifyName(ITextModel model, string newName)
        {
            string name = model.GetName();
            Text text = _db.Texts.FirstOrDefault(x => x.Name == name);
            if (text == null)
            {
                throw new ArgumentException("Cannot update model since no corresponding text exists in the database.");
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Cannot update model because new name is null or white space.");
            }
            if (_db.Texts.Any(x => x.Name == newName))
            {
                throw new ArgumentException($"Cannot update model since a text already exists with the name {newName}");
            }
            text.Name = newName;
            _db.SaveChanges();
        }

        public void ModifyIncludeQuotes(ITextModel model, bool includeQuotes)
        {
            string name = model.GetName();
            Text text = _db.Texts.FirstOrDefault(x => x.Name == name);
            if (text == null)
            {
                throw new ArgumentException("Cannot update model since no corresponding text exists in the database.");
            }
            text.IncludeQuotes = Convert.ToInt32(includeQuotes);
            _db.SaveChanges();
        }

        /// <summary>
        /// Takes a count model and returns a new Count database entity.
        /// </summary>
        /// <param name="model">The count model.</param>
        /// <returns>A Count database entity.</returns>
        private WordCount TranslateCounts(ISingleWordCountModel model)
        {
            if (model.GetLength() != UniversalConstants.CountSize)
            {
                throw new ArgumentException($"Please pass a model with length {UniversalConstants.CountSize}.");
            }
            WordCount output = new WordCount()
            {
                One = model.GetAt(0),
                Two = model.GetAt(1),
                Three = model.GetAt(2),
                Four = model.GetAt(3),
                Five = model.GetAt(4),
                Six = model.GetAt(5),
                Seven = model.GetAt(6),
                Eight = model.GetAt(7),
                Nine = model.GetAt(8),
                Ten = model.GetAt(9),
                Eleven = model.GetAt(10),
                Twelve = model.GetAt(11),
                Thirteen = model.GetAt(12)
            };
            return output;
        }

        /// <summary>
        /// Takes a Count database entity and returns a count model.
        /// </summary>
        /// <param name="count">The Count database entity.</param>
        /// <returns>A count model.</returns>
        private ISingleWordCountModel TranslateCounts(WordCount count)
        {
            ISingleWordCountModel output = _modelFactory.GetSingleCountModel(UniversalConstants.CountSize);
            output.SetAt(0, (int)count.One);
            output.SetAt(1, (int)count.Two);
            output.SetAt(2, (int)count.Three);
            output.SetAt(3, (int)count.Four);
            output.SetAt(4, (int)count.Five);
            output.SetAt(5, (int)count.Six);
            output.SetAt(6, (int)count.Seven);
            output.SetAt(7, (int)count.Eight);
            output.SetAt(8, (int)count.Nine);
            output.SetAt(9, (int)count.Ten);
            output.SetAt(10, (int)count.Eleven);
            output.SetAt(11, (int)count.Twelve);
            output.SetAt(12, (int)count.Thirteen);
            return output;
        }

        /// <summary>
        /// Takes a Text database entity and returns a flexible count model representing the
        /// text's two sets of counts (with and without quotations).
        /// </summary>
        /// <param name="text">The Text entity.</param>
        /// <returns>A flexible count model with two sets of counts (with and without quotations).</returns>
        private IFlexibleWordCountModel GetCountsFromText(Text text)
        {
            WordCount withQuotes = _db.WordCounts.FirstOrDefault(x => x.Id == text.WithQuotesId);
            WordCount withoutQuotes = _db.WordCounts.FirstOrDefault(x => x.Id == text.WithoutQuotesId);
            if (withQuotes == null || withoutQuotes == null)
            {
                throw new ArgumentException("One of the Counts entities is missing.");
            }
            ISingleWordCountModel withQuotesModel = TranslateCounts(withQuotes);
            ISingleWordCountModel withoutQuotesModel = TranslateCounts(withoutQuotes);
            return _modelFactory.GetFlexibleCountModel(withQuotesModel, withoutQuotesModel);
        }
    }
}
