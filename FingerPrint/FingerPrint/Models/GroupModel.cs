using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models
{
    public class GroupModel : ICountableItem, INamedItem
    {
        public string Name { get; set; }
        private List<INamedCountableItem> _contents;
        private SingleWordCountModel _counts;

        public GroupModel(string name)
        {
            Name = name;
            _contents = new List<INamedCountableItem>();


        }

        public void AddItem(INamedCountableItem item)
        {
            _contents.Add(item);
        }

        public void DeleteByName(string name)
        {
            INamedCountableItem item = _contents.FirstOrDefault(x => string.Equals(name, x.GetName(), StringComparison.CurrentCultureIgnoreCase));
            if (item == null)
            {
                throw new ArgumentException($"Group does not contain an item named {name}");
            }
            _contents.Remove(item);
        }

        private void CalculateFingerprint()
        {

        } 

        public SingleWordCountModel GetCounts()
        {
            return _counts.Copy();
        }

        public string GetName()
        {
            return string.Copy(Name);
        }
    }
}
