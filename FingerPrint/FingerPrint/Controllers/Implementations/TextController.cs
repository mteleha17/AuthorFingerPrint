using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint;
using FingerPrint.Stores;
using FingerPrint.Models.Interfaces;

namespace FingerPrint.Controllers.Implementations
{
    public class TextController : ITextController
    {
        private List<ITextViewModel> _textTempDb;
        private List<IGroupViewModel> _groupTempDb;
        //private ITextStore _textStore;
        //private IGroupStore _groupStore;
        private IModelFactory _modelFactory;

        public TextController(List<ITextViewModel> textTempDb,
            List<IGroupViewModel> groupTempDb, 
            ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            _textTempDb = textTempDb;
            _groupTempDb = groupTempDb;
            //_textStore = textStore;
            //_groupStore = groupStore;
            _modelFactory = modelFactory;
        }

        public ITextViewModel GetTextByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }

            //return _textStore.GetOne(x => x.Name == name);
            return _textTempDb.FirstOrDefault(x => x.GetName() == name);
        }

        public bool AnyByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }
            //return _textStore.Any(x => x.Name == name);
            return _textTempDb.Any(x => x.GetName() == name);
        }

        public List<ITextViewModel> GetTextByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            //return _textStore.GetMany(x => x.Author == author).Select(x => (ITextViewModel)x).ToList();
            return _textTempDb.Where(x => x.GetAuthor() == author).Select(x => (ITextViewModel)x).ToList();
        }

        public bool AnyByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            //return _textStore.Any(x => x.Author == author);
            return _textTempDb.Any(x => x.GetAuthor() == author);
        }

        public ITextViewModel CreateText(string name, TextReader input, int length, string author = null)
        {
            if (AnyByName(name) || _groupTempDb.Any(x => x.GetName() == name))
            {
                throw new ArgumentException($"Cannot create text because another item in the database already has the name {name}.");
            }
            ITextModel model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            //_textStore.Add(model);
            _textTempDb.Add(model);
            return model;
        }

        public void DeleteText(ITextViewModel model)
        {
            //_textStore.Delete((ITextModel)model);
            _textTempDb.Remove(model);
        }

        public void UpdateText(ITextViewModel model, string name = null, string author = null, bool? includeQuotes = null)
        {
            var updatedModel = (ITextModel)model;
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedModel.SetName(name);
                //_textStore.ModifyName((ITextModel)model, name);
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                updatedModel.SetAuthor(author);
                //_textStore.ModifyAuthor((ITextModel)model, author);

            }
            if (includeQuotes != null)
            {
                updatedModel.SetIncludeQuotes((bool)includeQuotes);
                //_textStore.ModifyIncludeQuotes((ITextModel)model, (bool)includeQuotes);
            }
        }

        public List<ITextViewModel> GetAllTexts()
        {
            //return _textStore.GetMany(x => true).Select(x => (ITextViewModel)x).ToList();
            return _textTempDb.Where(x => true).Select(x => (ITextViewModel)x).ToList();
        }
    }
}
