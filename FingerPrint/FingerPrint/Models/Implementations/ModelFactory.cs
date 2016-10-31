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
    public class ModelFactory : IModelFactory
    {
        public ITextModel GetTextModel(string name, TextReader input, int length)
        {
            var counts = GetFlexibleCountModel(length);
            GenerateCounts(input, counts);
            return new TextModel(name, counts);
        }

        public ITextModel GetTextModel(string name, IFlexibleWordCountModel counts)
        {
            if (counts == null)
            {
                throw new ArgumentException("Counts model must not be null.");
            }
            if (counts.GetLength() < 1)
            {
                throw new ArgumentException("Counts model must not have length 0.");
            }
            return new TextModel(name, counts);
        }

        public IGroupModel GetGroupModel(string name, int length)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must not be null or whitespace.");
            }
            var counts = GetSingleCountModel(length);
            return new GroupModel(name, counts);
        }

        public ISingleWordCountModel GetSingleCountModel(int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("The number of counts must be positive.");
            }
            var counts = new int[length];
            return GetSingleCountModel(counts);
        }

        public ISingleWordCountModel GetSingleCountModel(int[] counts)
        {
            if (counts == null)
            {
                throw new ArgumentException("Array of counts must not be null.");
            }
            if (counts.Length < 1)
            {
                throw new ArgumentException("The number of counts must be positive.");
            }
            foreach (int i in counts)
            {
                if (i < 0)
                {
                    throw new ArgumentException("Counts must not be negative.");
                }
            }
            return new SingleWordCountModel(counts);
        }

        public IFlexibleWordCountModel GetFlexibleCountModel(int length)
        {
            var withQuotes = GetSingleCountModel(length);
            var withoutQuotes = GetSingleCountModel(length);
            return GetFlexibleCountModel(withQuotes, withoutQuotes);
        }

        public IFlexibleWordCountModel GetFlexibleCountModel(ISingleWordCountModel withQuotes, ISingleWordCountModel withoutQuotes)
        {
            if (withQuotes == withoutQuotes)
            {
                throw new ArgumentException("Please supply two different count models.");
            }
            if (withQuotes == null || withoutQuotes == null)
            {
                throw new ArgumentException("Array of counts must not be null.");
            }
            int withQuotesLength = withQuotes.GetLength();
            int withoutQuotesLength = withoutQuotes.GetLength();

            if (withQuotesLength < 1 || withoutQuotesLength < 1)
            {
                throw new ArgumentException("Number of counts must not be less than 1.");
            }
            if (withQuotesLength != withoutQuotesLength)
            {
                throw new ArgumentException("The arrays must have the same length.");
            }
            for (int i = 0; i < withQuotesLength; i++)
            {
                if (withQuotes.GetAt(i) < 0 || withoutQuotes.GetAt(i) < 0)
                {
                    throw new ArgumentException($"Counts must not be negative. Item {i} in one of the arrays was negative.");
                }
            }
            return new FlexibleWordCountModel(withQuotes, withoutQuotes);
        }

        //For test purposes. Comment out later.
        public void GenerateCountsTestMethod(TextReader text, IFlexibleWordCountModel model)
        {
            GenerateCounts(text, model);
        }

        private void GenerateCounts(TextReader text, IFlexibleWordCountModel model)
        {
            int[] countsWithQuotes = new int[10];
            int[] countsWithoutQuotes = new int[10];
            string delimPattern = @"\s+";
            Regex delim = new Regex(delimPattern);
            bool inQuotes = false;
            bool continueWord = false;
            string firstHalfOfWord = "";
            int previousWordLength = 0;
            bool mismatchedQuotationMarks = false;
            string line;

            while ((line = text.ReadLine()) != null) // read text file line by line until end of line
            {
                if (line.Length != 0) // skip line if empty
                {
                    string[] wordsArray = delim.Split(line.Trim()); // split the line using delimiter
                    for (int i = 0; i < wordsArray.Length; i++) // iterate through split array
                    {
                        string currentWord = wordsArray[i]; // grab a single word to count from split array
                        if (continueWord) // this conditional handles the case of if the previous line ends with a hyphen
                        {
                            if (currentWord[0] >= 'A' && currentWord[0] <= 'Z') // if the first letter of the current word is uppercase it means the previous hyphen was used incorrectly. don't change the current word
                            {
                                firstHalfOfWord = "";
                                continueWord = false;
                            }
                            else // append the previous word to the front of the current word if the hyphen was used correctly
                            {
                                currentWord = firstHalfOfWord + currentWord;
                                firstHalfOfWord = "";
                                continueWord = false;
                                if (inQuotes) // uncount last wordlength count from counts with quotes if currently inside of quotations
                                {
                                    countsWithQuotes[previousWordLength - 1]--;
                                }
                                else // uncount last wordlength count for both counts if current outside of quotations
                                {
                                    countsWithQuotes[previousWordLength - 1]--;
                                    countsWithoutQuotes[previousWordLength - 1]--;
                                }
                            }
                        }
                        // if the last word of the line ends with a hyphen, store the word in a variable, removing the hyphen
                        if (i == wordsArray.Length - 1)
                        {
                            if (currentWord[currentWord.Length - 1] == '-')
                            {
                                firstHalfOfWord = currentWord.Substring(0, currentWord.Length - 1);
                                continueWord = true;
                            }
                        }
                        // if it locates a starting quotation mark, set as in quotations
                        if (currentWord[0] == '"' || currentWord[0] == '“')
                        {
                            inQuotes = true;
                        }
                        string tempCurrentWord = Regex.Replace(currentWord, "[\"]", ""); // remove quotes from the current word
                        tempCurrentWord = Regex.Replace(tempCurrentWord, "[^a-zA-Z0-9']+$", ""); // remove non-alphanumeric characters from the end of the word except for apostrophes
                        Debug.Print(tempCurrentWord);
                        if (!(tempCurrentWord.Length == 0))
                        {
                            int wordLength = tempCurrentWord.Length;
                            previousWordLength = wordLength;
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
                        // if it locates an ending quotation mark, set as no longer within quotations
                        if (currentWord[currentWord.Length - 1] == '"' || currentWord[currentWord.Length - 1] == '”')
                        {
                            inQuotes = false;
                        }
                        
                    }
                }
            }
            if (inQuotes)
            {
                mismatchedQuotationMarks = true;
            }
            // set wordlength counts for the model
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
