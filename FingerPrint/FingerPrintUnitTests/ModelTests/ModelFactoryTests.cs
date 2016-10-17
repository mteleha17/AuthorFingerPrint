using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint.Models;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class ModelFactoryTests
    {
        private IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> _modelFactory;

        StringReader stringReader;
        StreamReader streamReader;
        ISingleWordCountModel counts;

        [TestInitialize]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();
            counts = _modelFactory.GetSingleCountModel(10);
        }

        [TestMethod]
        public void GetSingleCountsByLength()
        {
            ISingleWordCountModel output = _modelFactory.GetSingleCountModel(10);
            Assert.AreEqual(output.GetLength(), 10);
        }

        [TestMethod]
        public void CountWithQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, 10);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(1, counts.GetAt(1));
            Assert.AreEqual(1, counts.GetAt(2));
            Assert.AreEqual(2, counts.GetAt(3));
            Assert.AreEqual(2, counts.GetAt(4));
            Assert.AreEqual(1, counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(1, counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
        }

        [TestMethod]
        public void CountWithoutQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, 10);
            textModel.SetIncludeQuotes(false);
            counts = textModel.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(1, counts.GetAt(1));
            Assert.AreEqual(0, counts.GetAt(2));
            Assert.AreEqual(2, counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
            Assert.AreEqual(1, counts.GetAt(5));
            Assert.AreEqual(0, counts.GetAt(6));
            Assert.AreEqual(0, counts.GetAt(7));
            Assert.AreEqual(1, counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
        }

        [TestMethod]
        public void CountWordAcrossLines()
        {
            string s = "Let us consider a word spanning multip-\nle lines. Will the program handle it correctly?";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, 10);
            counts = textModel.GetCounts();
            Assert.AreEqual(1, counts.GetAt(0));
            Assert.AreEqual(2, counts.GetAt(1));
            Assert.AreEqual(2, counts.GetAt(2));
            Assert.AreEqual(2, counts.GetAt(3));
            Assert.AreEqual(1, counts.GetAt(4));
            Assert.AreEqual(1, counts.GetAt(5));
            Assert.AreEqual(1, counts.GetAt(6));
            Assert.AreEqual(3, counts.GetAt(7));
            Assert.AreEqual(1, counts.GetAt(8));
            Assert.AreEqual(0, counts.GetAt(9));
        }

        [TestMethod]
        public void CountWordAcrossLinesFromFile()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\WordSpanningMultipleLines.txt"))
            {
                var textModel = _modelFactory.GetTextModel("test", streamReader, 10);
                counts = textModel.GetCounts();
                Assert.AreEqual(1, counts.GetAt(0));
                Assert.AreEqual(2, counts.GetAt(1));
                Assert.AreEqual(2, counts.GetAt(2));
                Assert.AreEqual(2, counts.GetAt(3));
                Assert.AreEqual(1, counts.GetAt(4));
                Assert.AreEqual(1, counts.GetAt(5));
                Assert.AreEqual(1, counts.GetAt(6));
                Assert.AreEqual(3, counts.GetAt(7));
                Assert.AreEqual(1, counts.GetAt(8));
                Assert.AreEqual(0, counts.GetAt(9));
            }
        }

        [TestMethod]
        public void CountMismatchedQuotationMarks()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\MismatchedQuotationMarks.txt"))
            {
                var textModel = _modelFactory.GetTextModel("test", streamReader, 10);
                counts = textModel.GetCounts();
                Assert.AreEqual(0, counts.GetAt(0));
                Assert.AreEqual(0, counts.GetAt(1));
                Assert.AreEqual(1, counts.GetAt(2));
                Assert.AreEqual(2, counts.GetAt(3));
                Assert.AreEqual(2, counts.GetAt(4));
                Assert.AreEqual(0, counts.GetAt(5));
                Assert.AreEqual(0, counts.GetAt(6));
                Assert.AreEqual(0, counts.GetAt(7));
                Assert.AreEqual(1, counts.GetAt(8));
                Assert.AreEqual(1, counts.GetAt(9));
            }
        }

        [TestMethod]
        public void CountScrambledText()
        {
            int length = 10;
            ITextModel<ISingleWordCountModel> modelUnscrambled;
            ITextModel<ISingleWordCountModel> modelScrambled;
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
        }
    }
}
