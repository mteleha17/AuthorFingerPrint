using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class WordCountModelFactoryTests
    {
        StringReader stringReader;
        StreamReader streamReader;
        IFlexibleWordCountModel<ISingleWordCountModel> counts;
        IWordCountModelFactory<IFlexibleWordCountModel<ISingleWordCountModel>> factory;

        [TestInitialize]
        public void Initialize()
        {
            int[] withQuotes = new int[10];
            int[] withoutQuotes = new int[10];
            ISingleWordCountModel countsWithQuotes = new SingleWordCountModel(withQuotes);
            ISingleWordCountModel countsWithoutQuotes = new SingleWordCountModel(withoutQuotes);
            counts = new FlexibleWordCountModel(countsWithQuotes, countsWithoutQuotes);
            factory = new WordCountModelFactory();
        }

        [TestMethod]
        public void CountWithQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            factory.GenerateCounts(stringReader, counts);
            Assert.AreEqual(0, counts.GetAt(true, 0));
            Assert.AreEqual(1, counts.GetAt(true, 1));
            Assert.AreEqual(1, counts.GetAt(true, 2));
            Assert.AreEqual(2, counts.GetAt(true, 3));
            Assert.AreEqual(2, counts.GetAt(true, 4));
            Assert.AreEqual(1, counts.GetAt(true, 5));
            Assert.AreEqual(0, counts.GetAt(true, 6));
            Assert.AreEqual(0, counts.GetAt(true, 7));
            Assert.AreEqual(1, counts.GetAt(true, 8));
            Assert.AreEqual(0, counts.GetAt(true, 9));
        }

        [TestMethod]
        public void CountWithoutQuotes()
        {
            string s = "As Thomas Jefferson once said, \"Hey there buddy.\"";
            stringReader = new StringReader(s);
            factory.GenerateCounts(stringReader, counts);
            Assert.AreEqual(0, counts.GetAt(false, 0));
            Assert.AreEqual(1, counts.GetAt(false, 1));
            Assert.AreEqual(0, counts.GetAt(false, 2));
            Assert.AreEqual(2, counts.GetAt(false, 3));
            Assert.AreEqual(0, counts.GetAt(false, 4));
            Assert.AreEqual(1, counts.GetAt(false, 5));
            Assert.AreEqual(0, counts.GetAt(false, 6));
            Assert.AreEqual(0, counts.GetAt(false, 7));
            Assert.AreEqual(1, counts.GetAt(false, 8));
            Assert.AreEqual(0, counts.GetAt(false, 9));
        }

        [TestMethod]
        public void CountWordAcrossLines()
        {
            string s = "Let's consider a word spanning multip-\nle lines. Will the program handle it correctly?";
            stringReader = new StringReader(s);
            factory.GenerateCounts(stringReader, counts);
            Assert.AreEqual(1, counts.GetAt(true, 0));
            Assert.AreEqual(1, counts.GetAt(true, 1));
            Assert.AreEqual(1, counts.GetAt(true, 2));
            Assert.AreEqual(2, counts.GetAt(true, 3));
            Assert.AreEqual(2, counts.GetAt(true, 4));
            Assert.AreEqual(1, counts.GetAt(true, 5));
            Assert.AreEqual(1, counts.GetAt(true, 6));
            Assert.AreEqual(3, counts.GetAt(true, 7));
            Assert.AreEqual(1, counts.GetAt(true, 8));
            Assert.AreEqual(0, counts.GetAt(true, 9));
        }

        [TestMethod]
        public void CountWordAcrossLinesFromFile()
        {
            using (streamReader = new StreamReader("..\\..\\SampleTextFiles\\WordSpanningMultipleLines.txt"))
            {
                factory.GenerateCounts(streamReader, counts);
                Assert.AreEqual(1, counts.GetAt(true, 0));
                Assert.AreEqual(1, counts.GetAt(true, 1));
                Assert.AreEqual(1, counts.GetAt(true, 2));
                Assert.AreEqual(2, counts.GetAt(true, 3));
                Assert.AreEqual(2, counts.GetAt(true, 4));
                Assert.AreEqual(1, counts.GetAt(true, 5));
                Assert.AreEqual(1, counts.GetAt(true, 6));
                Assert.AreEqual(3, counts.GetAt(true, 7));
                Assert.AreEqual(1, counts.GetAt(true, 8));
                Assert.AreEqual(0, counts.GetAt(true, 9));
            }
        }

    }
}
