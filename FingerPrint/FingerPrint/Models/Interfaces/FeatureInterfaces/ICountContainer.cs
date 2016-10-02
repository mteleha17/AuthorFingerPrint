using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces
{
    /// <summary>
    /// Interface to be implemented by a class that contains a collection of word-length counts. 
    /// </summary>
    public interface ICountContainer
    {
        int[] Counts();
    }
}
