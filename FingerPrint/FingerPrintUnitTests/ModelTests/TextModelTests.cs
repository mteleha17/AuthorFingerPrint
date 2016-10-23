using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class TextModelTests
    {
        private IModelFactory _modelFactory;

        IFlexibleWordCountModel wordCountModel, nullWordCountModel;
        ITextModel model;

        [TestInitialize]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();

            int[] countsWithQuotes = new int[] { 0, 3, 1, 4, 2, 5};
            int[] countsWithoutQuotes = new int[] { 6, 9, 7, 10, 8, 11 };
            ISingleWordCountModel withQuotes = _modelFactory.GetSingleCountModel(countsWithQuotes);
            ISingleWordCountModel withoutQuotes = _modelFactory.GetSingleCountModel(countsWithoutQuotes);
            wordCountModel = _modelFactory.GetFlexibleCountModel(withQuotes, withoutQuotes);
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
            model.SetName("Gary");
            Assert.AreEqual("Gary", model.GetName());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetNameNull()
        {
            model.SetName(null);
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
            model.SetIncludeQuotes(true);
            ISingleWordCountModel counts = model.GetCounts();
            Assert.AreEqual(counts.GetAt(3), 4);
        }

        [TestMethod]
        public void GetCountsWithoutQuotes()
        {
            model.SetIncludeQuotes(false);
            ISingleWordCountModel counts = model.GetCounts();
            Assert.AreEqual(counts.GetAt(3), 10);
        }
    }
}
