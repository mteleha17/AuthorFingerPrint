using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using FingerPrint.Stores;
using System.Collections.Generic;
using FingerPrint.Models.Implementations;
using System.Linq;
using FingerPrint.AuxiliaryClasses;
using FingerPrint.Models.Interfaces;
using System.IO;
using FingerPrint.Models;

namespace FingerPrintUnitTests.StoreTests
{
    [TestClass]
    public class GroupStoreTests
    {
        private FingerprintLite13Entities _db;
        private IModelFactory _modelFactory;
        private ITextStore _textStore;
        private IGroupStore _groupStore;
        private Stack<string> _uniqueNames;

        [TestInitialize]
        public void Initialize()
        {
            _db = new FingerprintLite13Entities();
            _modelFactory = new ModelFactory();
            _textStore = new TextStore(_db, _modelFactory);
            _groupStore = new GroupStore(_db, _modelFactory, _textStore);
            _uniqueNames = new Stack<string>();

            //Assuming we will never need more than 2 group names per method
            int namesNeeded = 2 * (this.GetType()).GetMethods().Count();
            GenerateNames(namesNeeded, _uniqueNames);
        }

        private void GenerateNames(int targetCount, Stack<string> names)
        {
            int count = 0;
            while (count < targetCount)
            {
                int tries = 0;
                string name = "a";
                int unit = 'b' - 'a';
                Random random = new Random();
                while (_db.Texts.Any(x => x.Name == name) || names.Any(x => x == name))
                {
                    if (tries > 15)
                    {
                        throw new Exception("What are the odds?");
                    }
                    double nextRandom = random.NextDouble();
                    int increase = ((int)(nextRandom * 26)) * unit;
                    name += ('a' + increase);
                    ++tries;
                }
                names.Push(name);
                ++count;
            }
        }

        private void CompareTextModels(ITextModel model1, ITextModel model2)
        {
            Assert.AreEqual(model1.GetName(), model2.GetName());
            Assert.AreEqual(model1.GetAuthor(), model2.GetAuthor());
            Assert.AreEqual(model1.GetIncludeQuotes(), model2.GetIncludeQuotes());
            ISingleWordCountModel counts1 = model1.GetCounts();
            ISingleWordCountModel counts2 = model1.GetCounts();
            ISingleWordCountModel withQuotes1 = model1.GetCountsWithQuotes();
            ISingleWordCountModel withQuotes2 = model2.GetCountsWithQuotes();
            ISingleWordCountModel withoutQuotes1 = model1.GetCountsWithoutQuotes();
            ISingleWordCountModel withoutQuotes2 = model2.GetCountsWithoutQuotes();
            for (int i = 0; i < withQuotes1.GetLength(); i++)
            {
                Assert.AreEqual(counts1.GetAt(i), counts2.GetAt(i));
                Assert.AreEqual(withQuotes1.GetAt(i), withQuotes2.GetAt(i));
                Assert.AreEqual(withoutQuotes1.GetAt(i), withoutQuotes2.GetAt(i));
            }
            Assert.AreEqual(model1.GetLength(), model2.GetLength());
        }

        [TestMethod]
        public void TestAdd()
        {
            string name = _uniqueNames.Pop();
            IGroupModel model = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(model);
            Grouping result = _db.Groupings.FirstOrDefault(x => x.Name == name);
            Assert.IsNotNull(result);
            if (result != null)
            {
                _db.Groupings.Remove(result);
                _db.SaveChanges();
            }
        }

        [TestMethod]
        public void TestAddText()
        {
            string groupName = _uniqueNames.Pop();
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            string textName = _uniqueNames.Pop();
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            _groupStore.AddItem(groupModel, textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            List<ITextOrGroupViewModel> groupMembers = groupModel.GetMembers();
            Assert.AreEqual(1, groupMembers.Count);
            CompareTextModels(textModel, (ITextModel)groupMembers[0]);
            _groupStore.RemoveItem(groupModel, textModel);
            _textStore.Delete(textModel);
            _groupStore.Delete(groupModel);
        }

        [TestMethod]
        public void TestAddGroup()
        {
            string name1 = _uniqueNames.Pop();
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            string name2 = _uniqueNames.Pop();
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group2);
            _groupStore.AddItem(group1, group2);
            group1 = _groupStore.GetOne(x => x.Name == name1);
            List<ITextOrGroupViewModel> groupMembers = group1.GetMembers();
            Assert.AreEqual(1, groupMembers.Count);
            Assert.AreEqual(name2, groupMembers[0].GetName());
            Assert.IsInstanceOfType(groupMembers[0], typeof(IGroupModel));
            _groupStore.RemoveItem(group1, group2);
            _groupStore.Delete(group2);
            _groupStore.Delete(group1);
        }

        [TestMethod]
        public void TestAddItems()
        {
            string name1 = _uniqueNames.Pop();
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            string name2 = _uniqueNames.Pop();
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group2);
            string textName = _uniqueNames.Pop();
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            List<ITextOrGroupModel> membersToAdd = new List<ITextOrGroupModel>()
            {
                group2, textModel
            };
            _groupStore.AddItems(group1, membersToAdd);
            group1 = _groupStore.GetOne(x => x.Name == name1);
            List<ITextOrGroupViewModel> groupMembers = group1.GetMembers();
            Assert.AreEqual(2, groupMembers.Count);
            int textIndex = 0;
            int groupIndex = 0;
            if (groupMembers[0] is ITextModel)
            {
                textIndex = 0;
                groupIndex = 1;
            }
            else
            {
                textIndex = 1;
                groupIndex = 0;
            }
            Assert.IsInstanceOfType(groupMembers[textIndex], typeof(ITextModel));
            CompareTextModels(textModel, (ITextModel)groupMembers[textIndex]);
            Assert.AreEqual(name2, groupMembers[groupIndex].GetName());
            Assert.IsInstanceOfType(groupMembers[groupIndex], typeof(IGroupModel));
            _groupStore.RemoveItem(group1, group2);
            _groupStore.RemoveItem(group1, textModel);
            _textStore.Delete(textModel);
            _groupStore.Delete(group2);
            _groupStore.Delete(group1);
        }

        //[TestMethod]
        //public void TestContainsOneLevel()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestDelete()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestExists()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestGetOne()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestGetMany()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestIsChild()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestIsParent()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestModifyName()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestRemoveItem()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestRemoveItems()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
