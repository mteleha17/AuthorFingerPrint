﻿using FingerPrint.Models;
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
            Count withQuotes = TranslateCounts(model.GetCountsWithQuotes());
            Count withoutQuotes = TranslateCounts(model.GetCountsWithoutQuotes());
            _db.Counts.Add(withQuotes);
            _db.Counts.Add(withoutQuotes);
            Text text = new Text()
            {
                Name = model.GetName(),
                Author = model.GetAuthor(),
                CountsWithQuotesID = withQuotes.CountsID,
                CountsWithoutQuotesID = withoutQuotes.CountsID,
            };
            _db.Texts.Add(text);
            _db.SaveChanges();
        }

        public void Delete(ITextModel model)
        {
            Text text = _db.Texts.FirstOrDefault(x => x.Name == model.GetName());
            if (text == null)
            {
                throw new ArgumentException($"Cannot delete text {model.GetName()} since it does not exist in the database.");
            }
            if (_db.Text_Group.Any(x => x.TextID == text.TextID))
            {
                throw new ArgumentException($"Cannot delete text {model.GetName()} because it is currently a member of a group.");
            }
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
                yield return GetOne(x => x.TextID == text.TextID);
            }
        }

        public ITextModel GetOne(Expression<Func<Text, bool>> criteria)
        {
            Text text = _db.Texts.FirstOrDefault(criteria);
            if (text == null)
            {
                return null;
            }
            IFlexibleWordCountModel counts = GetCountsFromText(text); 
            return _modelFactory.GetTextModel(text.Name, counts);
        }

        public void ModifyAuthor(ITextModel model, string newAuthor)
        {
            Text text = _db.Texts.FirstOrDefault(x => x.Name == model.GetName());
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
            Text text = _db.Texts.FirstOrDefault(x => x.Name == model.GetName());
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
            Text text = _db.Texts.FirstOrDefault(x => x.Name == model.GetName());
            if (text == null)
            {
                throw new ArgumentException("Cannot update model since no corresponding text exists in the database.");
            }
            text.QuoteInd = includeQuotes;
            _db.SaveChanges();
        }

        private Count TranslateCounts(ISingleWordCountModel model)
        {
            if (model.GetLength() != UniversalCountSize.CountSize)
            {
                throw new ArgumentException($"Please pass a model with length {UniversalCountSize.CountSize}.");
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

        private IFlexibleWordCountModel GetCountsFromText(Text text)
        {
            Count withQuotes = _db.Counts.FirstOrDefault(x => x.CountsID == text.CountsWithQuotesID);
            Count withoutQuotes = _db.Counts.FirstOrDefault(x => x.CountsID == text.CountsWithoutQuotesID);
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
