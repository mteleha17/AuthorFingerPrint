using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;

namespace FingerPrint.Controllers.Implementations
{
    public class AnalysisController : IAnalysisController
    {
        private List<IGroupViewModel> _activeGroups;

        public AnalysisController()
        {
            _activeGroups = new List<IGroupViewModel>();
        }

        public List<IGroupViewModel> GetActiveGroups()
        {
            return _activeGroups.ToList();
        }

        public void AddToActiveGroups(IGroupViewModel group)
        {
            if (GroupIsActive(group))
            {
                throw new ArgumentException("Cannot add group to active groups since group is already active.");
            }
            _activeGroups.Add(group);
        }

        public void RemoveFromActiveGroups(IGroupViewModel group)
        {
            if (!GroupIsActive(group))
            {
                throw new ArgumentException("Cannot remove group from active groups since it is not active.");
            }
            _activeGroups.Remove(group);
        }

        public bool GroupIsActive(IGroupViewModel group)
        {
            return _activeGroups.Contains(group);
        }
    }
}
