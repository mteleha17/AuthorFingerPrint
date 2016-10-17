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
            Assert.AreEqual(output.Length(), 10);
        }

        [TestMethod]
        public void CountWithQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            var textModel = _modelFactory.GetTextModel("test", stringReader, 10);
            counts = textModel.Counts();
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
            textModel.IncludeQuotes = false;
            counts = textModel.Counts();
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
            counts = textModel.Counts();
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
                counts = textModel.Counts();
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
                counts = textModel.Counts();
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
    }
}
