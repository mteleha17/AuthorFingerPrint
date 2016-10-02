using FingerPrint.Models.Interfaces;
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
    public class TextModel : ITextOrGroup
    {
        private readonly int _length;
        private string _name;
        private IFlexibleWordCountModel _counts;

        public string Author { get; set; }
        public bool IncludeQuotes { get; set; }

        public TextModel(string name, IFlexibleWordCountModel counts)
        {
            if (counts == null)
            {
                throw new ArgumentException("counts must not be null.");
            }
            _name = name;
            _counts = counts.Copy();
            _length = _counts.Length();
            IncludeQuotes = true;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public int Length()
        {
            return _length;
        }

        public int[] Counts()
        {
            if (IncludeQuotes)
            {
                return _counts.CountsWithQuotes();
            }
            else
            {
                return _counts.CountsWithoutQuotes();
            }
        }
    }
}
