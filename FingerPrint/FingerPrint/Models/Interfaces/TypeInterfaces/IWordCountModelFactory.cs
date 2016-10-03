using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Models.Interfaces.TypeInterfaces
{
    /// <summary>
    /// Interface to be implemented by a class that can create a WordCountModel using a TextReader.
    /// </summary>
    /// <typeparam name="WordCountModelType">The type of the word count model.</typeparam>
    public interface IWordCountModelFactory<WordCountModelType>
    {
        WordCountModelType GenerateCounts(TextReader text, WordCountModelType model);
    }
}
