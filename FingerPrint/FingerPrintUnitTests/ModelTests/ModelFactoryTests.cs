using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint.Models;
using FingerPrint.Models.Implementations;
using FingerPrint.AuxiliaryClasses;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class ModelFactoryTests
    {
        private IModelFactory _modelFactory;

        StringReader stringReader;
        StreamReader streamReader;
        ISingleWordCountModel counts;
        int countSize = UniversalConstants.CountSize;
        int multiplier = UniversalConstants.ConstantMultiplier;

        [TestInitialize]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();
            counts = _modelFactory.GetSingleCountModel(countSize);
        }

        [TestMethod]
        public void GetSingleCountsByLength()
        {
            ISingleWordCountModel output = _modelFactory.GetSingleCountModel(countSize);
            Assert.AreEqual(output.GetLength(), countSize);
        }

        [TestMethod]
        public void CountIncludingQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(getFreq(1, 8), counts.GetAt(1));
            Assert.AreEqual(getFreq(1, 8), counts.GetAt(2));
            Assert.AreEqual(getFreq(2, 8), counts.GetAt(3));
            Assert.AreEqual(getFreq(2, 8), counts.GetAt(4));
            Assert.AreEqual(getFreq(1, 8), counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(getFreq(1, 8), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountExcludingQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            textModel.SetIncludeQuotes(false);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(1));
            Assert.AreEqual(0, counts.GetAt(2));
            Assert.AreEqual(getFreq(2, 5), counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountExcludingCurlyQuotes()
        {
            string s = "As Thomas Jefferson once said, “Hey there buddy.”";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            textModel.SetIncludeQuotes(false);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(1));
            Assert.AreEqual(0, counts.GetAt(2));
            Assert.AreEqual(getFreq(2, 5), counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(getFreq(1, 5), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountWordAcrossLines()
        {
            string s = "Let us consider a word spanning multip-\nle lines. Will the program handle it correctly?";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            counts = textModel.GetCounts();
            Assert.AreEqual(getFreq(1, 14), counts.GetAt(0));
            Assert.AreEqual(getFreq(2, 14), counts.GetAt(1));
            Assert.AreEqual(getFreq(2, 14), counts.GetAt(2));
            Assert.AreEqual(getFreq(2, 14), counts.GetAt(3));
            Assert.AreEqual(getFreq(1, 14), counts.GetAt(4));
            Assert.AreEqual(getFreq(1, 14), counts.GetAt(5));
            Assert.AreEqual(getFreq(1, 14), counts.GetAt(6));
            Assert.AreEqual(getFreq(3, 14), counts.GetAt(7));
            Assert.AreEqual(getFreq(1, 14), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountExcludingQuotesWithWordAcrossLines()
        {
            string s = "Let us consider a word spanning \"multip-\nle lines.\" Will the program handle it correctly?";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            textModel.SetIncludeQuotes(false);
            counts = textModel.GetCounts();
            Assert.AreEqual(getFreq(1, 12), counts.GetAt(0));
            Assert.AreEqual(getFreq(2, 12), counts.GetAt(1));
            Assert.AreEqual(getFreq(2, 12), counts.GetAt(2));
            Assert.AreEqual(getFreq(2, 12), counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(getFreq(1, 12), counts.GetAt(5));
            Assert.AreEqual(getFreq(1, 12), counts.GetAt(6));
            Assert.AreEqual(getFreq(2, 12), counts.GetAt(7));
            Assert.AreEqual(getFreq(1, 12), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountWordAcrossLinesFromFile()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\WordSpanningMultipleLines.txt"))
            {
                var textModel = _modelFactory.GetTextModel("test", streamReader, countSize);
                counts = textModel.GetCounts();
                Assert.AreEqual(getFreq(1, 14), counts.GetAt(0));
                Assert.AreEqual(getFreq(2, 14), counts.GetAt(1));
                Assert.AreEqual(getFreq(2, 14), counts.GetAt(2));
                Assert.AreEqual(getFreq(2, 14), counts.GetAt(3));
                Assert.AreEqual(getFreq(1, 14), counts.GetAt(4));
                Assert.AreEqual(getFreq(1, 14), counts.GetAt(5));
                Assert.AreEqual(getFreq(1, 14), counts.GetAt(6));
                Assert.AreEqual(getFreq(3, 14), counts.GetAt(7));
                Assert.AreEqual(getFreq(1, 14), counts.GetAt(8));
                Assert.AreEqual(0, counts.GetAt(9));
                Assert.AreEqual(0, counts.GetAt(10));
                Assert.AreEqual(0, counts.GetAt(11));
                Assert.AreEqual(0, counts.GetAt(12));
            }
        }

        [TestMethod]
        public void CountMismatchedQuotationMarks()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\MismatchedQuotationMarks.txt"))
            {
                var textModel = _modelFactory.GetTextModel("test", streamReader, countSize);
                counts = textModel.GetCounts();
                Assert.AreEqual(0, counts.GetAt(0));
                Assert.AreEqual(0, counts.GetAt(1));
                Assert.AreEqual(getFreq(1, 7), counts.GetAt(2));
                Assert.AreEqual(getFreq(2, 7), counts.GetAt(3));
                Assert.AreEqual(getFreq(2, 7), counts.GetAt(4));
                Assert.AreEqual(0, counts.GetAt(5));
                Assert.AreEqual(0, counts.GetAt(6));
                Assert.AreEqual(0, counts.GetAt(7));
                Assert.AreEqual(getFreq(1, 7), counts.GetAt(8));
                Assert.AreEqual(getFreq(1, 7), counts.GetAt(9));
                Assert.AreEqual(0, counts.GetAt(10));
                Assert.AreEqual(0, counts.GetAt(11));
                Assert.AreEqual(0, counts.GetAt(12));
            }
        }

        [TestMethod]
        public void CountScrambledText()
        {
            int length = countSize;
            ITextModel modelUnscrambled;
            ITextModel modelScrambled;
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\ATaleOfTwoCitiesNormal.txt"))
            {
                modelUnscrambled = _modelFactory.GetTextModel("unscrambled", streamReader, length);
            }
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\ATaleOfTwoCitiesScrambled.txt"))
            {
                modelScrambled = _modelFactory.GetTextModel("scrambled", streamReader, length);
            }
            ISingleWordCountModel countsUnscrambled = modelUnscrambled.GetCounts();
            ISingleWordCountModel countsScrambled = modelScrambled.GetCounts();
            Assert.AreEqual(countsUnscrambled.GetAt(0), countsScrambled.GetAt(0));
            Assert.AreEqual(countsUnscrambled.GetAt(1), countsScrambled.GetAt(1));
            Assert.AreEqual(countsUnscrambled.GetAt(2), countsScrambled.GetAt(2));
            Assert.AreEqual(countsUnscrambled.GetAt(3), countsScrambled.GetAt(3));
            Assert.AreEqual(countsUnscrambled.GetAt(4), countsScrambled.GetAt(4));
            Assert.AreEqual(countsUnscrambled.GetAt(5), countsScrambled.GetAt(5));
            Assert.AreEqual(countsUnscrambled.GetAt(6), countsScrambled.GetAt(6));
            Assert.AreEqual(countsUnscrambled.GetAt(7), countsScrambled.GetAt(7));
            Assert.AreEqual(countsUnscrambled.GetAt(8), countsScrambled.GetAt(8));
            Assert.AreEqual(countsUnscrambled.GetAt(9), countsScrambled.GetAt(9));
            Assert.AreEqual(countsUnscrambled.GetAt(10), countsScrambled.GetAt(10));
            Assert.AreEqual(countsUnscrambled.GetAt(11), countsScrambled.GetAt(11));
            Assert.AreEqual(countsUnscrambled.GetAt(12), countsScrambled.GetAt(12));
        }

        [TestMethod]
        public void CountWithLineIncorrectlyEndingWithHyphen()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\TheSleeperExcerpt.txt"))
            {
                var textModel = _modelFactory.GetTextModel("test", streamReader, countSize);
                counts = textModel.GetCounts();
                Assert.AreEqual(0, counts.GetAt(0));
                Assert.AreEqual(0, counts.GetAt(1));
                Assert.AreEqual(getFreq(6, 15), counts.GetAt(2));
                Assert.AreEqual(getFreq(4, 15), counts.GetAt(3));
                Assert.AreEqual(getFreq(4, 15), counts.GetAt(4));
                Assert.AreEqual(getFreq(1, 15), counts.GetAt(5));
                Assert.AreEqual(0, counts.GetAt(6));
                Assert.AreEqual(0, counts.GetAt(7));
                Assert.AreEqual(0, counts.GetAt(8));
                Assert.AreEqual(0, counts.GetAt(9));
                Assert.AreEqual(0, counts.GetAt(10));
                Assert.AreEqual(0, counts.GetAt(11));
                Assert.AreEqual(0, counts.GetAt(12));
            }
        }

        [TestMethod]
        public void CountWithLineIncorrectlyEndingWithHyphenInsideQuotes()
        {
            string s = "The general shouted, \"No-\nWe will not give them an inch.";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, 13);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(getFreq(3, 11), counts.GetAt(1));
            Assert.AreEqual(getFreq(2, 11), counts.GetAt(2));
            Assert.AreEqual(getFreq(4, 11), counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(0, counts.GetAt(5));
            Assert.AreEqual(getFreq(2, 11), counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(0, counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountExcludingQuotesWithLineIncorrectlyEndingWithHyphenInsideQuotes()
        {
            string s = "The general shouted, \"No-\nWe will not give them an inch.";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            textModel.SetIncludeQuotes(false);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(0, counts.GetAt(1));
            Assert.AreEqual(getFreq(1, 3), counts.GetAt(2));
            Assert.AreEqual(0, counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(0, counts.GetAt(5));
            Assert.AreEqual(getFreq(2, 3), counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(0, counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        [TestMethod]
        public void CountWithEmDashes()
        {
            string s = "So fitfully—so fearfully— So fitfully—so fearfully—";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, countSize);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0)); 
            Assert.AreEqual(getFreq(4, 8), counts.GetAt(1));
            Assert.AreEqual(0, counts.GetAt(2));
            Assert.AreEqual(0, counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(0, counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(getFreq(2, 8), counts.GetAt(7));
            Assert.AreEqual(getFreq(2, 8), counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
            Assert.AreEqual(0, counts.GetAt(10));
            Assert.AreEqual(0, counts.GetAt(11));
            Assert.AreEqual(0, counts.GetAt(12));
        }

        public int getFreq(int wordCount, int totalWords) 
        {
            return (int)(((double)wordCount / totalWords) * multiplier);
        }

    }
}
