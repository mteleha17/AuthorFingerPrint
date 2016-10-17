﻿using FingerPrint.Models.Interfaces.TypeInterfaces;
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
    public class ModelFactory : IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>>
    {
        public ITextModel<ISingleWordCountModel> GetTextModel(string name, TextReader input, int length)
        {
            var counts = GetFlexibleCountModel(length);
            GenerateCounts(input, counts);
            return new TextModel(name, counts);
        }

        public ITextModel<ISingleWordCountModel> GetTextModel(string name, IFlexibleWordCountModel<ISingleWordCountModel> counts)
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

        public IGroupModel<ISingleWordCountModel> GetGroupModel(string name, int length)
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

        public IFlexibleWordCountModel<ISingleWordCountModel> GetFlexibleCountModel(int length)
        {
            var withQuotes = GetSingleCountModel(length);
            var withoutQuotes = GetSingleCountModel(length);
            return GetFlexibleCountModel(withQuotes, withoutQuotes);
        }

        public IFlexibleWordCountModel<ISingleWordCountModel> GetFlexibleCountModel(ISingleWordCountModel withQuotes, ISingleWordCountModel withoutQuotes)
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