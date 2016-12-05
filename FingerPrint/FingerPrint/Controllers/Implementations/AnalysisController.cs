using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;
using FingerPrint.AuxiliaryClasses;

namespace FingerPrint.Controllers.Implementations
{
    public class AnalysisController : IAnalysisController
    {
        private List<ITextOrGroupViewModel> _activeItems;

        public AnalysisController()
        {
            _activeItems = new List<ITextOrGroupViewModel>();
        }

        public List<ITextOrGroupViewModel> GetActiveItems()
        {
            return _activeItems.ToList();
        }

        public void AddToActiveItems(ITextOrGroupViewModel item)
        {
            if (ItemIsActive(item))
            {
                throw new ArgumentException("Cannot add group to active groups since group is already active.");
            }
            _activeItems.Add(item);
        }

        public void RemoveFromActiveItems(ITextOrGroupViewModel item)
        {
            if (!ItemIsActive(item))
            {
                throw new ArgumentException("Cannot remove group from active groups since it is not active.");
            }
            _activeItems.Remove(item);
        }

        public bool ItemIsActive(ITextOrGroupViewModel item)
        {
            return _activeItems.Contains(item);
        }
    }
}
