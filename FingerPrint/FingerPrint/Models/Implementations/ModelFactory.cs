using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FingerPrint.Models.Implementations
{
    public class ModelFactory : IModelFactory<ISingleWordCountModel>
    {
        public ITextModel<ISingleWordCountModel> GetTextModel(string name, TextReader input, int numberWordLengths)
        {
            var withQuotes = new SingleWordCountModel(numberWordLengths);
            var withoutQuotes = new SingleWordCountModel(numberWordLengths);
            var counts = new FlexibleWordCountModel(withQuotes, withoutQuotes);
            GenerateCounts(input, counts);
            return new TextModel(name, counts);
        }

        public IGroupModel<ISingleWordCountModel> GetGroupModel(string name, int numberWordLengths)
        {
            var counts = new SingleWordCountModel(numberWordLengths);
            return new GroupModel(name, counts);
        }

        //For test purposes. Comment out later.
        public void GenerateCountsTestMethod(TextReader text, IFlexibleWordCountModel<ISingleWordCountModel> model)
        {
            GenerateCounts(text, model);
        }

        private void GenerateCounts(TextReader text, IFlexibleWordCountModel<ISingleWordCountModel> model)
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
                    for (int i = 0; i < wordsArray.Length; i++)
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
