using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class GroupModel : INamedCountableItem
    {
        private readonly int _length;
        private ISingleWordCountModel _counts;
        private List<INamedCountableItem> _items;

        public string Name { get; set; }

        public GroupModel(string name, ISingleWordCountModel wordCountModel)
        {
            Name = name;
            _counts = wordCountModel.Copy();
            _length = _counts.Length();
            _items = new List<INamedCountableItem>();
        }

        public int Length()
        {
            return _length;
        }

        public ISingleWordCountModel Counts()
        {
            return _counts.Copy();
        }

        public void Add(INamedCountableItem item)
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

        public void Delete(INamedCountableItem item)
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
                _counts[i] = 0;
            }
            foreach (INamedCountableItem item in _items)
            {
                int[] itemCounts = item.Counts().Counts();
                for (int i = 0; i < _length; i++)
                {
                    _counts[i] += itemCounts[i];
                }
            }
            int numberOfItems = _items.Count;
            for (int i = 0; i < _length; i++)
            {
                _counts[i] /= numberOfItems;
            }
        } 

        public string GetName()
        {
            return Name;
        }
    }
}