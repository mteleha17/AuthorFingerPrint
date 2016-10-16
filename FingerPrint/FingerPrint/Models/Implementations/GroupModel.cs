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
        private bool _modified;
        private readonly int _length;
        private string _name;
        private ISingleWordCountModel _counts;
        private List<ITextOrGroupModel<ISingleWordCountModel>> _items;

        public event EventHandler Modified;

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

        public GroupModel(string name, ISingleWordCountModel wordCountModel)
        {
            if (wordCountModel == null)
            {
                throw new ArgumentException("wordCountModel must not be null.");
            }
            for (int i = 0; i < wordCountModel.Length(); i++)
            {
                if (wordCountModel.GetAt(i) != 0)
                {
                    throw new ArgumentException("wordCountModel must have counts all equal to zero upon initializing group.");
                }
            }
            Name = name;
            _counts = wordCountModel.Copy();
            _length = _counts.Length();
            _items = new List<ITextOrGroupModel<ISingleWordCountModel>>();
            _modified = true;
        }

        public int Length()
        {
            return _length;
        }

        private void OnModified(object sender, EventArgs e)
        {
            if (Modified != null)
            {
                Modified(this, e); 
            }
            _modified = true;
        }

        public ISingleWordCountModel Counts()
        {
            if (_modified)
            {
                CalculateFingerprint();
                _modified = false;
            }
            return _counts.Copy();
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
            if (item is IGroupModel<ISingleWordCountModel>)
            {
                ((IGroupModel<ISingleWordCountModel>)item).Modified += new EventHandler(OnModified);
            }
            OnModified(this, EventArgs.Empty);
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
            if (item is IGroupModel<ISingleWordCountModel>)
            {
                ((IGroupModel<ISingleWordCountModel>)item).Modified -= new EventHandler(OnModified);
            }
            _items.Remove(item);
            OnModified(this, EventArgs.Empty);
        }

        public bool Contains(ITextOrGroupModel<ISingleWordCountModel> item)
        {
            return _items.Contains(item);
        }

        private void CalculateFingerprint()
        {
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, 0);
            }
            if (_items.Count == 0)
            {
                return;
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