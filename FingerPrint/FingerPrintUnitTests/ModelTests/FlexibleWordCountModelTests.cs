using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models;
using FingerPrintUnitTests.FakeModels;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class FlexibleWordCountModelTests
    {
        private IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> _modelFactory;
        ISingleWordCountModel singleCountIncludeQuotes, singleCountExcludeQuotes, singleCountNull,
            singleCountLengthZeroA, singleCountLengthZeroB, singleCountLengthOne,
            singleCountNegativeCount;
        IFlexibleWordCountModel<ISingleWordCountModel> flexibleWordCountModel;

        [TestInitialize()]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();
            int[] countsOne = new int[] { 1, 2, 3, 4, 5};
            singleCountIncludeQuotes = _modelFactory.GetSingleCountModel(countsOne);
            int[] countsTwo = new int[] { 1, 14, 3, 28, 5 };
            singleCountExcludeQuotes = _modelFactory.GetSingleCountModel(countsTwo);
            singleCountNull = null;
            int[] countsLengthOne = new int[] { 55 };
            singleCountLengthOne = _modelFactory.GetSingleCountModel(countsLengthOne);
            int[] countsLengthZeroA = new int[0];
            singleCountLengthZeroA = new FakeSingleWordCountModel(countsLengthZeroA);
            int[] countsLengthZeroB = new int[0];
            singleCountLengthZeroB = new FakeSingleWordCountModel(countsLengthZeroB);
            int[] countsWithNegative = new int[] { 1, -2, 3, 4, 5 };
            singleCountNegativeCount = new FakeSingleWordCountModel(countsWithNegative);
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountIncludeQuotes, singleCountExcludeQuotes);
        }

        [TestMethod]
        public void ValidConstruction()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountIncludeQuotes, singleCountExcludeQuotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullInput()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountNull, singleCountExcludeQuotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorDuplicateInput()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountIncludeQuotes, singleCountIncludeQuotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorLengthZeroInput()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountLengthZeroA, singleCountLengthZeroB);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInputUnequalLength()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountLengthOne, singleCountExcludeQuotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInputNegativeCount()
        {
            flexibleWordCountModel = _modelFactory.GetFlexibleCountModel(singleCountNegativeCount, singleCountExcludeQuotes);
        }

        [TestMethod]
        public void ValidGetAt()
        {
            int x = flexibleWordCountModel.GetAt(true, 3);
            Assert.AreEqual(x, 4);
            x = flexibleWordCountModel.GetAt(false, 3);
            Assert.AreEqual(x, 28);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtNegativeIndexWithQuotes()
        {
            var x = flexibleWordCountModel.GetAt(true, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtNegativeIndexWithoutQuotes()
        {
            var x = flexibleWordCountModel.GetAt(false, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtIndexEqualsLengthWithQuotes()
        {
            var x = flexibleWordCountModel.GetAt(true, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtIndexEqualsLengthWithoutQuotes()
        {
            var x = flexibleWordCountModel.GetAt(false, 5);
        }

        [TestMethod]
        public void ValidSetAt()
        {
            flexibleWordCountModel.SetAt(true, 3, 999);
            int x = flexibleWordCountModel.GetAt(true, 3);
            Assert.AreEqual(x, 999);
            x = flexibleWordCountModel.GetAt(false, 3);
            Assert.AreEqual(x, 28);
            flexibleWordCountModel.SetAt(false, 3, 888);
            x = flexibleWordCountModel.GetAt(false, 3);
            Assert.AreEqual(x, 888);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtNegativeIndexWithQuotes()
        {
            flexibleWordCountModel.SetAt(true, -1, 15);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtNegativeIndexWithoutQuotes()
        {
            flexibleWordCountModel.SetAt(false, -1, 16);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtIndexEqualsLengthWithQuotes()
        {
            flexibleWordCountModel.SetAt(true, 5, 17);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtIndexEqualsLengthWithoutQuotes()
        {
            flexibleWordCountModel.SetAt(false, 5, 18);
        }
    }
}
