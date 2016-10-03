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
    public class TextModel : ITextModel<ISingleWordCountModel>
    {
        private readonly int _length;
        private string _name;
        private IFlexibleWordCountModel<ISingleWordCountModel> _counts;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name must not be null or whitespace.");
                }
                _name = value;
            }
        }
        public string Author { get; set; }
        public bool IncludeQuotes { get; set; }

        public TextModel(string name, IFlexibleWordCountModel<ISingleWordCountModel> counts)
        {
            if (counts == null)
            {
                throw new ArgumentException("counts must not be null.");
            }
            Name = name;
            _counts = counts.Copy();
            _length = _counts.Length();
            IncludeQuotes = true;
        }

        public int Length()
        {
            return _length;
        }

        public ISingleWordCountModel Counts()
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
