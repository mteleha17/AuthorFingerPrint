using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Models.Implementations;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class GroupModelTests
    {
        private IModelFactory _modelFactory;

        ITextModel textOne, textTwo, textThree, textWrongLength;
        IGroupModel groupOne, groupTwo;
        ISingleWordCountModel badCountInitialization;

        [TestInitialize]
        public void Initialize()
        {
            _modelFactory = new ModelFactory();

            var countsOne = new int[] { 1, 2, 3, 4, 5 };
            var countsTwo = new int[] { 7, 8, 9, 10, 11 };
            var countsThree = new int[] { 100, 101, 102, 103, 104 };
            var countsWrongLength = new int[] { 55 };

            var groupCountsBadInitialization = new int[] { 7, 7, 7, 7, 7};

            var countsWithQuotesOne = _modelFactory.GetSingleCountModel(countsOne);
            var countsWithQuotesTwo = _modelFactory.GetSingleCountModel(countsTwo);
            var countsWithQuotesThree = _modelFactory.GetSingleCountModel(countsThree);
            var countsWithQuotesWrongLength = _modelFactory.GetSingleCountModel(countsWrongLength);

            var countsWithoutQuotesOne = _modelFactory.GetSingleCountModel((int[])countsOne.Clone());
            var countsWithoutQuotesTwo = _modelFactory.GetSingleCountModel((int[])countsTwo.Clone());
            var countsWithoutQuotesThree = _modelFactory.GetSingleCountModel((int[])countsThree.Clone());
            var countsWithoutQuotesWrongLength = _modelFactory.GetSingleCountModel((int[])countsWrongLength.Clone());

            badCountInitialization = _modelFactory.GetSingleCountModel(groupCountsBadInitialization);

            var flexibleCountsOne = _modelFactory.GetFlexibleCountModel(countsWithQuotesOne, countsWithoutQuotesOne);
            var flexibleCountsTwo = _modelFactory.GetFlexibleCountModel(countsWithQuotesTwo, countsWithoutQuotesTwo);
            var flexibleCountsThree = _modelFactory.GetFlexibleCountModel(countsWithQuotesThree, countsWithoutQuotesThree);
            var flexibleCountsWrongLength = _modelFactory.GetFlexibleCountModel(countsWithQuotesWrongLength, countsWithoutQuotesWrongLength);

            textOne = _modelFactory.GetTextModel("text one", flexibleCountsOne);
            textTwo = _modelFactory.GetTextModel("text two", flexibleCountsTwo);
            textThree = _modelFactory.GetTextModel("text three", flexibleCountsThree);
            textWrongLength = _modelFactory.GetTextModel("text wrong length", flexibleCountsWrongLength);

            groupOne = _modelFactory.GetGroupModel("group one", 5);
            groupTwo = _modelFactory.GetGroupModel("group two", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullWordCount()
        {
            groupOne = new GroupModel("group one", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorBadCountInitialization()
        {
            groupOne = new GroupModel("bad count initialization", badCountInitialization);
        }

        [TestMethod]
        public void ValidSetName()
        {
            groupOne.SetName("Gary");
            Assert.AreEqual("Gary", groupOne.GetName());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetNameNull()
        {
            groupOne.SetName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddGroupToItself()
        {
            groupOne.Add(groupOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNullItem()
        {
            groupOne.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOfWrongLength()
        {
            groupOne.Add(textWrongLength);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddRedundantItem()
        {
            try
            {
                groupOne.Add(textOne);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
            groupOne.Add(textOne);
        }

        [TestMethod]
        public void ValidDeletion()
        {
            groupOne.Add(textOne);
            groupOne.Remove(textOne);
            groupOne.Add(groupTwo);
            groupOne.Remove(groupTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteNull()
        {
            try
            {
                groupOne.Add(textOne);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            groupOne.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteNotPresent()
        {
            try
            {
                groupOne.Add(textOne);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            groupOne.Remove(textTwo);
        }

        [TestMethod]
        public void CountEmptyGroup()
        {
            var counts = groupOne.GetCounts();
            Assert.AreEqual(0, counts.GetAt(0));
            Assert.AreEqual(0, counts.GetAt(1));
            Assert.AreEqual(0, counts.GetAt(2));
            Assert.AreEqual(0, counts.GetAt(3));
            Assert.AreEqual(0, counts.GetAt(4));
        }

        [TestMethod]
        public void CountOneText()
        {
            groupOne.Add(textOne);
            ISingleWordCountModel counts = groupOne.GetCounts();
            Assert.AreEqual(1, counts.GetAt(0));
            Assert.AreEqual(2, counts.GetAt(1));
            Assert.AreEqual(3, counts.GetAt(2));
            Assert.AreEqual(4, counts.GetAt(3));
            Assert.AreEqual(5, counts.GetAt(4));
        }

        [TestMethod]
        public void CountTwoTexts()
        {
            groupOne.Add(textOne);
            groupOne.Add(textTwo);
            ISingleWordCountModel counts = groupOne.GetCounts();
            Assert.AreEqual(4, counts.GetAt(0));
            Assert.AreEqual(5, counts.GetAt(1));
            Assert.AreEqual(6, counts.GetAt(2));
            Assert.AreEqual(7, counts.GetAt(3));
            Assert.AreEqual(8, counts.GetAt(4));
        }

        [TestMethod]
        public void CountOneGroup()
        {
            groupOne.Add(textOne);
            groupOne.Add(textTwo);
            groupTwo.Add(groupOne);
            ISingleWordCountModel counts = groupTwo.GetCounts();
            Assert.AreEqual(4, counts.GetAt(0));
            Assert.AreEqual(5, counts.GetAt(1));
            Assert.AreEqual(6, counts.GetAt(2));
            Assert.AreEqual(7, counts.GetAt(3));
            Assert.AreEqual(8, counts.GetAt(4));
        }

        [TestMethod]
        public void CountOneGroupOneFile()
        {
            groupOne.Add(textOne);
            groupOne.Add(textTwo);
            groupTwo.Add(groupOne);
            groupTwo.Add(textThree);
            ISingleWordCountModel counts = groupTwo.GetCounts();
            Assert.AreEqual(52, counts.GetAt(0));
            Assert.AreEqual(53, counts.GetAt(1));
            Assert.AreEqual(54, counts.GetAt(2));
            Assert.AreEqual(55, counts.GetAt(3));
            Assert.AreEqual(56, counts.GetAt(4));
        }

        [TestMethod]
        public void CountBetweenAdds()
        {
            groupOne.Add(textOne);
            groupOne.Add(textTwo);
            groupTwo.Add(groupOne);
            ISingleWordCountModel counts = groupTwo.GetCounts();
            groupTwo.Add(textThree);
            counts = groupTwo.GetCounts();
            Assert.AreEqual(52, counts.GetAt(0));
            Assert.AreEqual(53, counts.GetAt(1));
            Assert.AreEqual(54, counts.GetAt(2));
            Assert.AreEqual(55, counts.GetAt(3));
            Assert.AreEqual(56, counts.GetAt(4));
        }

        [TestMethod]
        public void DeleteFromChild()
        {
            groupOne.Add(textOne);
            groupOne.Add(textTwo);
            groupTwo.Add(groupOne);
            ISingleWordCountModel counts = groupTwo.GetCounts();
            groupOne.Remove(textTwo);
            counts = groupTwo.GetCounts();
            Assert.AreEqual(1, counts.GetAt(0));
            Assert.AreEqual(2, counts.GetAt(1));
            Assert.AreEqual(3, counts.GetAt(2));
            Assert.AreEqual(4, counts.GetAt(3));
            Assert.AreEqual(5, counts.GetAt(4));
        }
    }
}