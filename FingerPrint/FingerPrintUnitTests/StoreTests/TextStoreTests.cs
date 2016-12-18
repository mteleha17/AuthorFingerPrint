using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FingerPrint;
using FingerPrint.Stores;
using FingerPrint.Models.Implementations;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint.AuxiliaryClasses;
using System.Collections.Generic;
using System.Linq;
using FingerPrint.Models;

namespace FingerPrintUnitTests.StoreTests
{
    [TestClass]
    public class TextStoreTests
    {
        private FingerprintLite13Entities _db;
        private IModelFactory _modelFactory;
        private ITextStore _textStore;
        private Stack<string> _uniqueNames;

        [TestInitialize]
        public void Initialize()
        {
            _db = new FingerprintLite13Entities();
            _modelFactory = new ModelFactory();
            _textStore = new TextStore(_db, _modelFactory);
            _uniqueNames = new Stack<string>();

            //Assuming we will never need more than 2 text names per method
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
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, text, UniversalConstants.CountSize);
            _textStore.Add(model);
            Text result = _db.Texts.FirstOrDefault(x => x.Name == name);
            Assert.IsNotNull(result);
            if (result != null)
            {
                _db.Texts.Remove(result);
                _db.SaveChanges();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicate()
        {
            ArgumentException expectedException = null;
            string name = _uniqueNames.Pop();
            try
            {
                StreamReader text1 = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
                ITextModel model1 = _modelFactory.GetTextModel(name, text1, UniversalConstants.CountSize);
                _textStore.Add(model1);
                StreamReader text2 = new StreamReader("../../SampleTextFiles/MismatchedQuotationMarks.txt");
                ITextModel model2 = _modelFactory.GetTextModel(name, text2, UniversalConstants.CountSize);
                _textStore.Add(model2);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {   
                List<Text> textsToDelete = _db.Texts.Where(x => x.Name == name).ToList();
                foreach (Text text in textsToDelete)
                {
                    _db.Texts.Remove(text);
                    _db.SaveChanges();
                }
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            string name = _uniqueNames.Pop();
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, text, UniversalConstants.CountSize);
            _textStore.Add(model);
            _textStore.Delete(model);
            Text result = _db.Texts.FirstOrDefault(x => x.Name == name);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNonexistant()
        {
            string name = _uniqueNames.Pop();
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, text, UniversalConstants.CountSize);
            _textStore.Delete(model);
        }

        [TestMethod]
        public void TestExists()
        {
            string name = _uniqueNames.Pop();
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, text, UniversalConstants.CountSize);
            bool exists = _textStore.Exists(x => x.Name == name);
            Assert.IsFalse(exists);
            _textStore.Add(model);
            exists = _textStore.Exists(x => x.Name == name);
            Assert.IsTrue(exists);
            _textStore.Delete(model);
            exists = _textStore.Exists(x => x.Name == name);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void TestGetOne()
        {
            string name = _uniqueNames.Pop();
            ITextModel model = _textStore.GetOne(x => x.Name == name);
            Assert.IsNull(model);
            StreamReader text = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            model = _modelFactory.GetTextModel(name, text, UniversalConstants.CountSize);
            _textStore.Add(model);
            ITextModel modelFromDb = _textStore.GetOne(x => x.Name == name);
            CompareTextModels(model, modelFromDb);
            _textStore.Delete(model);
        }

        [TestMethod]
        public void TestGetMany()
        {
            string name1 = _uniqueNames.Pop();
            string name2 = _uniqueNames.Pop();
            IEnumerable<ITextModel> modelsFromDb = _textStore.GetMany(x => x.Name == name1);
            List<ITextModel> models = modelsFromDb.ToList();
            Assert.AreEqual(0, models.Count);
            StreamReader text1 = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            StreamReader text2 = new StreamReader("../../SampleTextFiles/MismatchedQuotationMarks.txt");
            ITextModel model1 = _modelFactory.GetTextModel(name1, text1, UniversalConstants.CountSize);
            ITextModel model2 = _modelFactory.GetTextModel(name2, text2, UniversalConstants.CountSize);
            _textStore.Add(model1);
            _textStore.Add(model2);
            modelsFromDb = _textStore.GetMany(x => x.Name == name1 || x.Name == name2);
            models = modelsFromDb.ToList();
            CompareTextModels(model1, models[0]);
            CompareTextModels(model2, models[1]);
            _textStore.Delete(model1);
            _textStore.Delete(model2);
        }

        [TestMethod]
        public void TestIsChild()
        {
            string name = _uniqueNames.Pop();
            StreamReader sr = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, sr, UniversalConstants.CountSize);
            _textStore.Add(model);
            Assert.IsFalse(_textStore.IsChild(model));
            string textName = model.GetName();
            Text text = _db.Texts.FirstOrDefault(x => x.Name == textName);
            Grouping group = new Grouping() { Name = _uniqueNames.Pop()};
            _db.Groupings.Add(group);
            _db.SaveChanges();
            Text_Grouping tg = new Text_Grouping() { TextId = text.Id, GroupingId = group.Id};
            _db.Text_Grouping.Add(tg);
            _db.SaveChanges();
            Assert.IsTrue(_textStore.IsChild(model));
            _db.Text_Grouping.Remove(tg);
            _db.SaveChanges();
            _db.Groupings.Remove(group);
            _db.SaveChanges();
            _textStore.Delete(model);
        }

        [TestMethod]
        public void TestModifyAuthor()
        {
            string name = _uniqueNames.Pop();
            StreamReader sr = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, sr, UniversalConstants.CountSize);
            model.SetAuthor("tom");
            _textStore.Add(model);
            model = _textStore.GetOne(x => x.Name == name);
            Assert.AreEqual("tom", model.GetAuthor());
            _textStore.ModifyAuthor(model, "jones");
            model = _textStore.GetOne(x => x.Name == name);
            Assert.AreEqual("jones", model.GetAuthor());
            _textStore.Delete(model);
        }

        [TestMethod]
        public void TestModifyIncludeQuotes()
        {
            string name = _uniqueNames.Pop();
            StreamReader sr = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, sr, UniversalConstants.CountSize);
            model.SetIncludeQuotes(true);
            _textStore.Add(model);
            model = _textStore.GetOne(x => x.Name == name);
            Assert.IsTrue(model.GetIncludeQuotes());
            _textStore.ModifyIncludeQuotes(model, false);
            model = _textStore.GetOne(x => x.Name == name);
            Assert.IsFalse(model.GetIncludeQuotes());
            _textStore.Delete(model);
        }

        [TestMethod]
        public void TestModifyName()
        {
            string name = _uniqueNames.Pop();
            StreamReader sr = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
            ITextModel model = _modelFactory.GetTextModel(name, sr, UniversalConstants.CountSize);
            _textStore.Add(model);
            model = _textStore.GetOne(x => x.Name == name);
            Assert.IsNotNull(model);
            string newName = _uniqueNames.Pop();
            _textStore.ModifyName(model, newName);
            ITextModel nullModel = _textStore.GetOne(x => x.Name == name);
            model = _textStore.GetOne(x => x.Name == newName);
            Assert.IsNull(nullModel);
            Assert.IsNotNull(model);
            _textStore.Delete(model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModifyNameDuplicate()
        {
            ArgumentException expectedException = null;
            string name1 = _uniqueNames.Pop();
            string name2 = _uniqueNames.Pop();
            try
            {
                IEnumerable<ITextModel> modelsFromDb = _textStore.GetMany(x => x.Name == name1);
                List<ITextModel> models = modelsFromDb.ToList();
                Assert.AreEqual(0, models.Count);
                StreamReader text1 = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
                StreamReader text2 = new StreamReader("../../SampleTextFiles/MismatchedQuotationMarks.txt");
                ITextModel model1 = _modelFactory.GetTextModel(name1, text1, UniversalConstants.CountSize);
                ITextModel model2 = _modelFactory.GetTextModel(name2, text2, UniversalConstants.CountSize);
                _textStore.Add(model1);
                _textStore.Add(model2);
                _textStore.ModifyName(model1, name2);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                List<Text> textsToDelete = _db.Texts.Where(x => x.Name == name1 || x.Name == name2).ToList();
                foreach (Text text in textsToDelete)
                {
                    _db.Texts.Remove(text);
                    _db.SaveChanges();
                }
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
            string name = _uniqueNames.Pop();
            try
            {
                StreamReader sr = new StreamReader("../../SampleTextFiles/WordSpanningMultipleLines.txt");
                ITextModel model = _modelFactory.GetTextModel(name, sr, UniversalConstants.CountSize);
                _textStore.Add(model);
                model = _textStore.GetOne(x => x.Name == name);
                Assert.IsNotNull(model);
                string newName = _uniqueNames.Pop();
                _textStore.ModifyName(model, null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            finally
            {
                Text text = _db.Texts.FirstOrDefault(x => x.Name == name);
                _db.Texts.Remove(text);
                _db.SaveChanges();
            }
            if (expectedException != null)
            {
                throw expectedException;
            }
        }

    }
}
