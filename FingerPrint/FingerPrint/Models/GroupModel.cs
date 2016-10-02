using FingerPrint.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    /// <summary>
    /// Class representing a grouping of texts and other groups.
    /// </summary>
    public class GroupModel : IGroup
    {
        private readonly int _length;
        private string _name;
        private ISingleWordCountModel _counts;
        private List<ITextOrGroup> _items;

        public GroupModel(string name, ISingleWordCountModel wordCountModel)
        {
            _name = name;
            _counts = wordCountModel.Copy();
            _length = _counts.Length();
            _items = new List<ITextOrGroup>();
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
            return _counts.Counts();
        }

        public void Add(ITextOrGroup item)
        {
            if (item == null)
            {
                throw new ArgumentException("Cannot add null item.");
            }
            if (item.Length() != _length)
            {
                throw new ArgumentException("Countable item must have the same number of counts as the group to which it is added.");
            }
            _items.Add(item);
            CalculateFingerprint();
        }

        public void Delete(ITextOrGroup item)
        {
            if (item == null)
            {
                throw new ArgumentException("Cannot remove null item.");
            }
            if (!_items.Contains(item))
            {
                throw new ArgumentException($"Group does not contain item: {item}.");
            }
            _items.Remove(item);
            CalculateFingerprint();
        }

        private void CalculateFingerprint()
        {
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, 0);
            }
            foreach (ITextOrGroup item in _items)
            {
                int[] itemCounts = item.Counts();
                for (int i = 0; i < _length; i++)
                {
                    _counts.SetAt(i, _counts.GetAt(i) + itemCounts[i]);
                }
            }
            int numberOfItems = _items.Count;
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, _counts.GetAt(i) / numberOfItems);
            }
        }
    }
}