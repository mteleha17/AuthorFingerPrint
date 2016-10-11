using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.Diagnostics;
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
            bool inQuotes = false;
            bool continueWord = false;
            string firstHalfOfWord = "";
            string line;

            while ((line = text.ReadLine()) != null)
            {
                if (line.Length != 0)
                {
                    string[] wordsArray = rgx.Split(line.Trim());
                    for (int i = 0; i  < wordsArray.Length; i++)
                    {
                        string currentWord = wordsArray[i];
                        if (continueWord)
                        {
                            currentWord = firstHalfOfWord + currentWord;
                            firstHalfOfWord = "";
                            continueWord = false;
                        }
                        if (i == wordsArray.Length - 1)
                        {
                            if (currentWord[currentWord.Length - 1] == '-')
                            {
                                firstHalfOfWord = currentWord.Substring(0, currentWord.Length - 1);
                                continueWord = true;
                            }
                        }
                        if (!continueWord)
                        {
                            if (currentWord[0] == '"')
                            {
                                inQuotes = true;
                            }
                            string tempCurrentWord = Regex.Replace(currentWord, "[\"]", "");
                            tempCurrentWord = Regex.Replace(tempCurrentWord, "[^a-zA-Z0-9']+$", "");
                            Debug.Print(tempCurrentWord);
                            if (!(tempCurrentWord.Length == 0))
                            {
                                int wordLength = tempCurrentWord.Length;
                                if (!inQuotes)
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
                            if (currentWord[currentWord.Length - 1] == '"')
                            {
                                inQuotes = false;
                            }
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
        }
    }
}
