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
using System.Collections.Concurrent;

namespace FingerPrintUnitTests.StoreTests
{
    [TestClass]
    public class GroupStoreTests
    {
        private FingerprintLite13Entities _db;
        private IModelFactory _modelFactory;
        private ITextStore _textStore;
        private IGroupStore _groupStore;
        private ConcurrentStack<string> _uniqueNames;

        [TestInitialize]
        public void Initialize()
        {
            _db = new FingerprintLite13Entities();
            _modelFactory = new ModelFactory();
            _textStore = new TextStore(_db, _modelFactory);
            _groupStore = new GroupStore(_db, _modelFactory, _textStore);
            _uniqueNames = new ConcurrentStack<string>();

            //Assuming we will never need more than 2 group names per method
            int namesNeeded = 2 * (this.GetType()).GetMethods().Count();
            GenerateNames(namesNeeded, _uniqueNames);
        }

        private void GenerateNames(int targetCount, ConcurrentStack<string> names)
        {
            int count = 0;
            while (count < targetCount)
            {
                int tries = 0;
                string name = "GSTests";
                int unit = 'b' - 'a';
                Random random = new Random();
                while (_db.Texts.Any(x => x.Name == name) || _db.Groupings.Any(x => x.Name == name) || names.Any(x => x == name))
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
            string name;
            _uniqueNames.TryPop(out name);
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicate()
        {
            ArgumentException expectedException = null;
            string name;
            _uniqueNames.TryPop(out name);
            try
            {
                IGroupModel group1 = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
                _groupStore.Add(group1);
                IGroupModel group2 = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
                _groupStore.Add(group2);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                List<Grouping> groupsToDelete = _db.Groupings.Where(x => x.Name == name).ToList();
                foreach (Grouping group in groupsToDelete)
                {
                    _db.Groupings.Remove(group);
                    _db.SaveChanges();
                }
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        public void TestAddText()
        {
            string groupName;
            _uniqueNames.TryPop(out groupName);
            //string groupName = _uniqueNames.Pop();
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            _groupStore.AddItem(groupModel, textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            List<ITextOrGroupViewModel> groupMembers = groupModel.GetMembers();
            Assert.AreEqual(1, groupMembers.Count);
            Assert.AreEqual(textName, groupMembers[0].GetName());
            Assert.IsInstanceOfType(groupMembers[0], typeof(ITextModel));
            CompareTextModels(textModel, (ITextModel)groupMembers[0]);
            _groupStore.RemoveItem(groupModel, textModel);
            _textStore.Delete(textModel);
            _groupStore.Delete(groupModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicateText()
        {
            ArgumentException expectedException = null;
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            _groupStore.AddItem(groupModel, textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            try
            {
                _groupStore.AddItem(groupModel, textModel);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.RemoveItem(groupModel, textModel);
                _textStore.Delete(textModel);
                _groupStore.Delete(groupModel);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        public void TestAddGroup()
        {
            //string name1 = _uniqueNames.Pop();
            string name1;
            _uniqueNames.TryPop(out name1);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            //string name2 = _uniqueNames.Pop();
            string name2;
            _uniqueNames.TryPop(out name2);
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicateGroup()
        {
            ArgumentException expectedException = null;
            //string groupName = _uniqueNames.Pop();
            string groupName1;
            _uniqueNames.TryPop(out groupName1);
            IGroupModel groupModel1 = _modelFactory.GetGroupModel(groupName1, UniversalConstants.CountSize);
            _groupStore.Add(groupModel1);
            //string textName = _uniqueNames.Pop();
            string groupName2;
            _uniqueNames.TryPop(out groupName2);
            IGroupModel groupModel2 = _modelFactory.GetGroupModel(groupName2, UniversalConstants.CountSize);
            _groupStore.Add(groupModel2);
            _groupStore.AddItem(groupModel1, groupModel2);
            groupModel1 = _groupStore.GetOne(x => x.Name == groupName1);
            try
            {
                _groupStore.AddItem(groupModel1, groupModel2);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.RemoveItem(groupModel1, groupModel2);
                _groupStore.Delete(groupModel2);
                _groupStore.Delete(groupModel1);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddGroupToItself()
        {
            ArgumentException expectedException = null;
            string name1;
            _uniqueNames.TryPop(out name1);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            try
            {
                _groupStore.AddItem(group1, group1);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.Delete(group1);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddGroupToOwnChild()
        {
            ArgumentException expectedException = null;
            string name1;
            _uniqueNames.TryPop(out name1);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            string name2;
            _uniqueNames.TryPop(out name2);
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group2);
            _groupStore.AddItem(group1, group2);
            try
            {
                _groupStore.AddItem(group2, group1);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                if (_groupStore.Contains(group2, group1))
                {
                    _groupStore.RemoveItem(group2, group1);
                }
                _groupStore.RemoveItem(group1, group2);
                _groupStore.Delete(group2);
                _groupStore.Delete(group1);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddGroupToChildOfChild()
        {
            ArgumentException expectedException = null;
            string name1;
            _uniqueNames.TryPop(out name1);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            string name2;
            _uniqueNames.TryPop(out name2);
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group2);
            _groupStore.AddItem(group1, group2);
            string name3;
            _uniqueNames.TryPop(out name3);
            IGroupModel group3 = _modelFactory.GetGroupModel(name3, UniversalConstants.CountSize);
            _groupStore.Add(group3);
            _groupStore.AddItem(group2, group3);
            try
            {
                _groupStore.AddItem(group3, group1);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                if (_groupStore.Contains(group3, group1))
                {
                    _groupStore.RemoveItem(group2, group1);
                }
                _groupStore.RemoveItem(group2, group3);
                _groupStore.RemoveItem(group1, group2);
                _groupStore.Delete(group3);
                _groupStore.Delete(group2);
                _groupStore.Delete(group1);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        public void TestContainsOneLevel()
        {
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            Assert.IsFalse(_groupStore.Contains(groupModel, textModel));
            _groupStore.AddItem(groupModel, textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            Assert.IsTrue(_groupStore.Contains(groupModel, textModel));
            _groupStore.RemoveItem(groupModel, textModel);
            _textStore.Delete(textModel);
            _groupStore.Delete(groupModel);
        }

        [TestMethod]
        public void TestContainsTwoLevels()
        {
            //string name1 = _uniqueNames.Pop();
            string name1;
            _uniqueNames.TryPop(out name1);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            //string name2 = _uniqueNames.Pop();
            string name2;
            _uniqueNames.TryPop(out name2);
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group2);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            Assert.IsFalse(_groupStore.Contains(group1, textModel));
            _groupStore.AddItem(group2, textModel);
            Assert.IsFalse(_groupStore.Contains(group1, textModel));
            _groupStore.AddItem(group1, group2);
            Assert.IsTrue(_groupStore.Contains(group1, textModel));
            _groupStore.RemoveItem(group1, group2);
            _groupStore.RemoveItem(group2, textModel);
            _textStore.Delete(textModel);
            _groupStore.Delete(group2);
            _groupStore.Delete(group1);
        }

        [TestMethod]
        public void TestContainsSelf()
        {
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            Assert.IsFalse(_groupStore.Contains(groupModel, groupModel));
            _groupStore.Delete(groupModel);
        }

        [TestMethod]
        public void TestDelete()
        {
            //string name = _uniqueNames.Pop();
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel group = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(group);
            _groupStore.Delete(group);
            Assert.IsFalse(_groupStore.Exists(x => x.Name == name));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNonexistant()
        {
            //string name = _uniqueNames.Pop();
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel group = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Delete(group);
        }

        [TestMethod]
        public void TestExists()
        {
            //string name = _uniqueNames.Pop();
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel group = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            Assert.IsFalse(_groupStore.Exists(x => x.Name == name));
            _groupStore.Add(group);
            Assert.IsTrue(_groupStore.Exists(x => x.Name == name));
            _groupStore.Delete(group);
        }

        [TestMethod]
        public void TestGetOne()
        {
            //string name = _uniqueNames.Pop();
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel group = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(group);
            group = _groupStore.GetOne(x => x.Name == name);
            Assert.IsNotNull(group);
            Assert.AreEqual(group.GetName(), name);
            _groupStore.Delete(group);
            //name = _uniqueNames.Pop();
            _uniqueNames.TryPop(out name);
            group = _groupStore.GetOne(x => x.Name == name);
            Assert.IsNull(group);
        }

        [TestMethod]
        public void TestGetMany()
        {
            //string name1 = _uniqueNames.Pop();
            string name1;
            _uniqueNames.TryPop(out name1);
            //string name2 = _uniqueNames.Pop();
            string name2;
            _uniqueNames.TryPop(out name2);
            IGroupModel group1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            IGroupModel group2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(group1);
            _groupStore.Add(group2);
            IEnumerable<IGroupModel> models = _groupStore.GetMany(x => x.Name == name1 || x.Name == name2);
            List<IGroupModel> modelsList = models.ToList();
            Assert.AreEqual(2, modelsList.Count());
            if (modelsList[0].GetName() == name1)
            {
                Assert.AreEqual(modelsList[1].GetName(), name2);
            }
            else
            {
                Assert.AreEqual(modelsList[0].GetName(), name2);
                Assert.AreEqual(modelsList[1].GetName(), name1);
            }
            _groupStore.Delete(group1);
            _groupStore.Delete(group2);
        }

        [TestMethod]
        public void TestModifyName()
        {
            //string name = _uniqueNames.Pop();
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel model = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(model);
            model = _groupStore.GetOne(x => x.Name == name);
            //string name2 = _uniqueNames.Pop();
            string name2;
            _uniqueNames.TryPop(out name2);
            Assert.IsTrue(_groupStore.Exists(x => x.Name == name));
            Assert.IsFalse(_groupStore.Exists(x => x.Name == name2));
            _groupStore.ModifyName(model, name2);
            Assert.IsFalse(_groupStore.Exists(x => x.Name == name));
            Assert.IsTrue(_groupStore.Exists(x => x.Name == name2));
            model = _groupStore.GetOne(x => x.Name == name2);
            _groupStore.Delete(model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyNameDuplicate()
        {
            ArgumentException expectedException = null;
            string name1;
            _uniqueNames.TryPop(out name1);
            string name2;
            _uniqueNames.TryPop(out name2);
            IGroupModel model1 = _modelFactory.GetGroupModel(name1, UniversalConstants.CountSize);
            IGroupModel model2 = _modelFactory.GetGroupModel(name2, UniversalConstants.CountSize);
            _groupStore.Add(model1);
            _groupStore.Add(model2);
            model1 = _groupStore.GetOne(x => x.Name == name1);
            model2 = _groupStore.GetOne(x => x.Name == name2);
            try
            {
                _groupStore.ModifyName(model1, name2);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.Delete(model1);
                _groupStore.Delete(model2);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyNameNull()
        {
            ArgumentException expectedException = null;
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel model = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(model);
            model = _groupStore.GetOne(x => x.Name == name);
            try
            {
                _groupStore.ModifyName(model, null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.Delete(model);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyNameEmpty()
        {
            ArgumentException expectedException = null;
            string name;
            _uniqueNames.TryPop(out name);
            IGroupModel model = _modelFactory.GetGroupModel(name, UniversalConstants.CountSize);
            _groupStore.Add(model);
            model = _groupStore.GetOne(x => x.Name == name);
            try
            {
                _groupStore.ModifyName(model, "");
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.Delete(model);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        public void TestRemoveItem()
        {
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            _groupStore.AddItem(groupModel, textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            Assert.IsTrue(_groupStore.Contains(groupModel, textModel));
            _groupStore.RemoveItem(groupModel, textModel);
            Assert.IsFalse(_groupStore.Contains(groupModel, textModel));
            _textStore.Delete(textModel);
            _groupStore.Delete(groupModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveItemThatIsNotAMember()
        {
            ArgumentException expectedException = null;
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            _textStore.Add(textModel);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            try
            {
                _groupStore.RemoveItem(groupModel, textModel);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _textStore.Delete(textModel);
                _groupStore.Delete(groupModel);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveNonexistantItem()
        {
            ArgumentException expectedException = null;
            //string groupName = _uniqueNames.Pop();
            string groupName;
            _uniqueNames.TryPop(out groupName);
            IGroupModel groupModel = _modelFactory.GetGroupModel(groupName, UniversalConstants.CountSize);
            _groupStore.Add(groupModel);
            //string textName = _uniqueNames.Pop();
            string textName;
            _uniqueNames.TryPop(out textName);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel textModel = _modelFactory.GetTextModel(textName, text, UniversalConstants.CountSize);
            groupModel = _groupStore.GetOne(x => x.Name == groupName);
            try
            {
                _groupStore.RemoveItem(groupModel, textModel);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                _groupStore.Delete(groupModel);
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }
    }
}
