using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;

namespace FingerPrintUnitTests.ModelTests
{
    [TestClass]
    public class GroupModelTests
    {
        ITextModel<ISingleWordCountModel> textOne, textTwo, textThree, textWrongLength;
        IGroupModel<ISingleWordCountModel> groupOne, groupTwo;
        ISingleWordCountModel badCountInitialization;

        [TestInitialize]
        public void Initialize()
        {
            var countsOne = new int[] { 1, 2, 3, 4, 5 };
            var countsTwo = new int[] { 7, 8, 9, 10, 11 };
            var countsThree = new int[] { 100, 101, 102, 103, 104 };
            var countsWrongLength = new int[] { 55 };

            var groupCountsOne = new int[5];
            var groupCountsTwo = new int[5];
            var groupCountsBadInitialization = new int[] { 7, 7, 7, 7, 7};

            var countsWithQuotesOne = new SingleWordCountModel(countsOne);
            var countsWithQuotesTwo = new SingleWordCountModel(countsTwo);
            var countsWithQuotesThree = new SingleWordCountModel(countsThree);
            var countsWithQuotesWrongLength = new SingleWordCountModel(countsWrongLength);

            var countsWithoutQuotesOne = new SingleWordCountModel((int[])countsOne.Clone());
            var countsWithoutQuotesTwo = new SingleWordCountModel((int[])countsTwo.Clone());
            var countsWithoutQuotesThree = new SingleWordCountModel((int[])countsThree.Clone());
            var countsWithoutQuotesWrongLength = new SingleWordCountModel((int[])countsWrongLength.Clone());

            var singleGroupCountOne = new SingleWordCountModel(groupCountsOne);
            var singleGroupCountTwo = new SingleWordCountModel(groupCountsTwo);
            badCountInitialization = new SingleWordCountModel(groupCountsBadInitialization);

            var flexibleCountsOne = new FlexibleWordCountModel(countsWithQuotesOne, countsWithoutQuotesOne);
            var flexibleCountsTwo = new FlexibleWordCountModel(countsWithQuotesTwo, countsWithoutQuotesTwo);
            var flexibleCountsThree = new FlexibleWordCountModel(countsWithQuotesThree, countsWithoutQuotesThree);
            var flexibleCountsWrongLength = new FlexibleWordCountModel(countsWithQuotesWrongLength, countsWithoutQuotesWrongLength);

            textOne = new TextModel("text one", flexibleCountsOne);
            textTwo = new TextModel("text two", flexibleCountsTwo);
            textThree = new TextModel("text three", flexibleCountsThree);
            textWrongLength = new TextModel("text wrong length", flexibleCountsWrongLength);

            groupOne = new GroupModel("group one", singleGroupCountOne);
            groupTwo = new GroupModel("group two", singleGroupCountTwo);
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
            groupOne.Name = "Gary";
            Assert.AreEqual("Gary", groupOne.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetNameNull()
        {
            groupOne.Name = null;
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
            catch (Exception)
            {
                Assert.Fail();
            }
            groupOne.Add(textOne);
        }

        [TestMethod]
        public void ValidDeletion()
        {
            groupOne.Add(textOne);
            groupOne.Delete(textOne);
            groupOne.Add(groupTwo);
            groupOne.Delete(groupTwo);
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
            groupOne.Delete(null);
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
            groupOne.Delete(textTwo);
        }

        [TestMethod]
        public void CountEmptyGroup()
        {
            var counts = groupOne.Counts();
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
            ISingleWordCountModel counts = groupOne.Counts();
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
            ISingleWordCountModel counts = groupOne.Counts();
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
            ISingleWordCountModel counts = groupTwo.Counts();
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
            ISingleWordCountModel counts = groupTwo.Counts();
            Assert.AreEqual(52, counts.GetAt(0));
            Assert.AreEqual(53, counts.GetAt(1));
            Assert.AreEqual(54, counts.GetAt(2));
            Assert.AreEqual(55, counts.GetAt(3));
            Assert.AreEqual(56, counts.GetAt(4));
        }
    }
}