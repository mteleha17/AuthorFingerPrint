using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class SingleWordCountModelTests
    {
        private ISingleWordCountModel singleWordCountModel;
        private IModelFactory _modelFactory;

        [TestInitialize()]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();
            singleWordCountModel = _modelFactory.GetSingleCountModel(new int[] { 3, 5, 4, 7, 6 });
        }

        [TestMethod]
        public void ValidConstructionUsingIntArray()
        {
            int[] input = new int[4];
            ISingleWordCountModel model = new SingleWordCountModel(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorUsingNullArray()
        {
            ISingleWordCountModel model = new SingleWordCountModel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorUsingArrayLengthZero()
        {
            ISingleWordCountModel model = new SingleWordCountModel(new int[0]);
        }

        [TestMethod]
        public void TestCopy()
        {
            ISingleWordCountModel copy = singleWordCountModel.Copy();
            Assert.AreEqual(copy.GetAt(0), singleWordCountModel.GetAt(0));
            Assert.AreEqual(copy.GetAt(1), singleWordCountModel.GetAt(1));
            Assert.AreEqual(copy.GetAt(2), singleWordCountModel.GetAt(2));
            Assert.AreEqual(copy.GetAt(3), singleWordCountModel.GetAt(3));
            Assert.AreEqual(copy.GetAt(4), singleWordCountModel.GetAt(4));
        }

        [TestMethod]
        public void GetAtValidIndex()
        {
            int x = singleWordCountModel.GetAt(3);
            Assert.AreEqual(x, 7);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtIndexLessThanZero()
        {
            var x = singleWordCountModel.GetAt(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAtIndexEqualToLength()
        {
            var x = singleWordCountModel.GetAt(5);
        }

        [TestMethod]
        public void SetAtValidInputs()
        {
            singleWordCountModel.SetAt(2, 11);
            int x = singleWordCountModel.GetAt(2);
            Assert.AreEqual(x, 11);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtIndexLessThanZero()
        {
            singleWordCountModel.SetAt(-1, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetAtIndexEqualToLength()
        {
            singleWordCountModel.SetAt(5, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAtCountLessThanZero()
        {
            singleWordCountModel.SetAt(2, -1);
        }
    }
}
