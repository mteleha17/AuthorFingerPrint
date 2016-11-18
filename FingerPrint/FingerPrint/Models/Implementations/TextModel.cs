using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    /// <summary>
    /// Class representing a text.
    /// </summary>
    public class TextModel : ITextModel
    {
        private readonly int _length;
        private string _name;
        private string _author;
        private bool _includeQuotes;
        private IFlexibleWordCountModel _counts;

        public TextModel(string name, IFlexibleWordCountModel counts)
        {
            SetName(name);
            SetIncludeQuotes(true);
            if (counts == null)
            {
                throw new ArgumentException("counts must not be null.");
            }
            _counts = counts.Copy();
            _length = _counts.GetLength();
        }

        public int GetLength()
        {
            return _length;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must not be null or whitespace.");
            }
            _name = name;
        }

        public string GetAuthor()
        {
            return _author;
        }

        public void SetAuthor(string author)
        {
            _author = author;
        }

        public bool GetIncludeQuotes()
        {
            return _includeQuotes;
        }

        public void SetIncludeQuotes(bool value)
        {
            _includeQuotes = value;
        }

        public ISingleWordCountModel GetCounts()
        {
            if (GetIncludeQuotes())
            {
                return _counts.CountsWithQuotes();
            }
            else
            {
                return _counts.CountsWithoutQuotes();
            }
        }

        public ISingleWordCountModel GetCountsWithQuotes()
        {
            return _counts.CountsWithQuotes();
        }

        public ISingleWordCountModel GetCountsWithoutQuotes()
        {
            return _counts.CountsWithoutQuotes();
        }
    }
}
