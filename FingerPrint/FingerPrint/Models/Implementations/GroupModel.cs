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
    public class GroupModel : IGroupModel<ISingleWordCountModel>
    {
        private readonly int _length;
        private string _name;
        private ISingleWordCountModel _counts;
        private List<ITextOrGroupModel<ISingleWordCountModel>> _items;

        public string Name { get; set; }

        public GroupModel(string name, ISingleWordCountModel wordCountModel)
        {
            _name = name;
            _counts = wordCountModel.Copy();
            _length = _counts.Length();
            _items = new List<ITextOrGroupModel<ISingleWordCountModel>>();
        }

        public int Length()
        {
            return _length;
        }

        public ISingleWordCountModel Counts()
        {
            return _counts;
        }

        public void Add(ITextOrGroupModel<ISingleWordCountModel> item)
        {
            if (item == this)
            {
                throw new ArgumentException("It can't possibly be a good idea to add a group to itself.");
            }
            if (item == null)
            {
                throw new ArgumentException("Cannot add null item.");
            }
            if (item.Length() != _length)
            {
                throw new ArgumentException("Countable item must have the same length as the group to which it is added.");
            }
            if (_items.Contains(item))
            {
                throw new ArgumentException($"Item {item} cannot be added to a group that it is already a member of.");
            }
            _items.Add(item);
            CalculateFingerprint();
        }

        public void Delete(ITextOrGroupModel<ISingleWordCountModel> item)
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
            //reset counts
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, 0);
            }
            //get count totals
            foreach (ITextOrGroupModel<ISingleWordCountModel> item in _items)
            {
                var itemCounts = item.Counts();
                for (int i = 0; i < _length; i++)
                {
                    _counts.SetAt(i, _counts.GetAt(i) + itemCounts.GetAt(i));
                }
            }
            //divide by number of items to get averages
            int numberOfItems = _items.Count;
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, _counts.GetAt(i) / numberOfItems);
            }
        }
    }
}