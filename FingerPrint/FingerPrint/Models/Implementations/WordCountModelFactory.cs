using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace FingerPrint.Models.Implementations
{
    public class WordCountModelFactory : IWordCountModelFactory<IFlexibleWordCountModel<ISingleWordCountModel>>
    {
        public void GenerateCounts(TextReader text, IFlexibleWordCountModel<ISingleWordCountModel> model)
        {
            int[] countsWithQuotes = new int[10];
            int[] countsWithoutQuotes = new int[10];
            string pattern = @"\s+";
            Regex rgx = new Regex(pattern);
            bool quotesOn = false;
            string line;

            while ((line = text.ReadLine()) != null)
            {
                if (line != "")
                {
                    foreach (string word in rgx.Split(line.Trim()))
                    {
                        string currentWord = word;
                        if (currentWord[0] == '“')
                        {
                            quotesOn = true;
                        }
                        string tempCurrentWord = Regex.Replace(currentWord, "[“”]", "");
                        tempCurrentWord = Regex.Replace(tempCurrentWord, "[^a-zA-Z0-9']+$", "");
                        if (!(tempCurrentWord == ""))
                        {
                            int wordLength = tempCurrentWord.Length;
                            if (!quotesOn)
                            {
                                if (wordLength < countsWithQuotes.Length)
                                {
                                    countsWithQuotes[wordLength - 1]++;
                                    countsWithoutQuotes[wordLength - 1]++;
                                }
                                else
                                {
                                    countsWithQuotes[countsWithQuotes.Length - 1]++;
                                    countsWithoutQuotes[countsWithoutQuotes.Length - 1]++;
                                }
                            }
                            else
                            {
                                if (wordLength < countsWithQuotes.Length)
                                {
                                    countsWithQuotes[wordLength - 1]++;
                                }
                                else
                                {
                                    countsWithQuotes[countsWithQuotes.Length - 1]++;
                                }
                            }
                        }
                        if (currentWord[currentWord.Length - 1] == '”')
                        {
                            quotesOn = false;
                        }
                    }
                }
            }
            for (int i = 0; i < countsWithQuotes.Length; i++)
            {
                model.SetAt(true, i, countsWithQuotes[i]);
            }
            for (int i = 0; i < countsWithoutQuotes.Length; i++)
            {
                model.SetAt(false, i, countsWithoutQuotes[i]);
            }
            int xxxxxx = 5;
        }
    }
}
