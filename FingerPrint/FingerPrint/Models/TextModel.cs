using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class TextModel : INamedCountableItem
    {
        private readonly int _length;
        private IFlexibleWordCountModel _counts;

        public string Name { get; set; }
        public string Author { get; set; }
        public bool IncludeQuotes { get; set; }

        public TextModel(string name, IFlexibleWordCountModel wordCountModel)
        {
            if (wordCountModel == null)
            {
                throw new ArgumentException("wordCountModel must not be null.");
            }
            Name = string.Copy(name);
            _counts = wordCountModel.Copy();
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

        public string GetName()
        {
            return Name;
        }
    }
}
