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
    /// Class representing a grouping of texts and other groups.
    /// </summary>
    public class GroupModel : IGroupModel
    {
        private bool _modified;
        private readonly int _length;
        private string _name;
        private ISingleWordCountModel _counts;
        private List<ITextOrGroupModel> _items;

        public event EventHandler Modified;

        public GroupModel(string name, ISingleWordCountModel wordCountModel)
        {
            SetName(name);
            if (wordCountModel == null)
            {
                throw new ArgumentException("wordCountModel must not be null.");
            }
            for (int i = 0; i < wordCountModel.GetLength(); i++)
            {
                if (wordCountModel.GetAt(i) != 0)
                {
                    throw new ArgumentException("wordCountModel must have counts all equal to zero upon initializing group.");
                }
            }
            _items = new List<ITextOrGroupModel>();
            _counts = wordCountModel.Copy();
            _length = _counts.GetLength();
            _modified = true;
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

        /// <summary>
        /// Method to be called when the group is modified (i.e. a text or child group is removed from it).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModified(object sender, EventArgs e)
        {
            //Check to see if any other group has subscribed to this group's events.
            //Strangely, this can be determined by whether or not the Modified event is null.
            //If at least one group is subscribing, trigger the Modified event.
            if (Modified != null)
            {
                Modified(this, e); 
            }
            //In any case, this group has been modified and will need to recalculate its counts 
            //if and when it is asked for them.
            _modified = true;
        }

        public void Add(ITextOrGroupModel item)
        {
            if (item == this)
            {
                throw new ArgumentException("It can't possibly be a good idea to add a group to itself.");
            }
            if (item == null)
            {
                throw new ArgumentException("Cannot add null item.");
            }
            if (item.GetLength() != _length)
            {
                throw new ArgumentException("Countable item must have the same length as the group to which it is added.");
            }
            if (_items.Contains(item))
            {
                throw new ArgumentException($"Item {item} cannot be added to a group that it is already a member of.");
            }
            _items.Add(item);
            if (item is IGroupModel)
            {
                ((IGroupModel)item).Modified += new EventHandler(OnModified);
            }
            OnModified(this, EventArgs.Empty);
        }

        public void Remove(ITextOrGroupModel item)
        {
            if (item == null)
            {
                throw new ArgumentException("Cannot remove null item.");
            }
            if (!_items.Contains(item))
            {
                throw new ArgumentException($"Group does not contain item: {item}.");
            }
            if (item is IGroupModel)
            {
                ((IGroupModel)item).Modified -= new EventHandler(OnModified);
            }
            _items.Remove(item);
            OnModified(this, EventArgs.Empty);
        }

        public bool Contains(ITextOrGroupModel item)
        {
            return _items.Contains(item);
        }

        public List<ITextOrGroupViewModel> GetMembers()
        {
            return _items.Select(x => (ITextOrGroupViewModel)x).ToList();
        } 

        public ISingleWordCountModel GetCounts()
        {
            if (_modified)
            {
                CalculateFingerprint();
                _modified = false;
            }
            return _counts.Copy();
        }

        /// <summary>
        /// Recalculate this group's counts.
        /// </summary>
        private void CalculateFingerprint()
        {
            //Reset counts.
            for (int i = 0; i < _length; i++)
            {
                _counts.SetAt(i, 0);
            }
            //Check to see if this group has any texts or child groups as items.
            //If not, all counts can stay at zero.
            if (_items.Count == 0)
            {
                return;
            }
            //get count totals
            foreach (ITextOrGroupModel item in _items)
            {
                var itemCounts = item.GetCounts();
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