using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class TextModelTests
    {
        IFlexibleWordCountModel<ISingleWordCountModel> wordCountModel, nullWordCountModel;
        ITextModel<ISingleWordCountModel> model;

        [TestInitialize]
        public void Initialize()
        {
            int[] countsWithQuotes = new int[] { 0, 3, 1, 4, 2, 5};
            int[] countsWithoutQuotes = new int[] { 6, 9, 7, 10, 8, 11 };
            SingleWordCountModel withQuotes = new SingleWordCountModel(countsWithQuotes);
            SingleWordCountModel withoutQuotes = new SingleWordCountModel(countsWithoutQuotes);
            wordCountModel = new FlexibleWordCountModel(withQuotes, withoutQuotes);
            nullWordCountModel = null;

            model = new TextModel("model", wordCountModel);
        }

        [TestMethod]
        public void ValidConstruction()
        {
            TextModel validModel = new TextModel("model", wordCountModel);
        }

        [TestMethod]
        public void ValidSetName()
        {
            model.Name = "Gary";
            Assert.AreEqual("Gary", model.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetNameNull()
        {
            model.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullModel()
        {
            TextModel nullModel = new TextModel("model", nullWordCountModel);
        }

        [TestMethod]
        public void GetCountsWithQuotes()
        {
            model.IncludeQuotes = true;
            ISingleWordCountModel counts = model.Counts();
            Assert.AreEqual(counts.GetAt(3), 4);
        }

        [TestMethod]
        public void GetCountsWithoutQuotes()
        {
            model.IncludeQuotes = false;
            ISingleWordCountModel counts = model.Counts();
            Assert.AreEqual(counts.GetAt(3), 10);
        }
    }
}
