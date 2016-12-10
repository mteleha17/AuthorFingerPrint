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
        private List<ITextViewModel> _temporaryDatabase;
        private ITextStore _textStore;//group2
        private IGroupStore _groupStore;//group2
        private IModelFactory _modelFactory;

        public TextController(ITextStore textStore,
            IGroupStore groupStore,
            IModelFactory modelFactory)
        {
            _temporaryDatabase = new List<ITextViewModel>();
            _textStore = textStore;//group2
            _groupStore = groupStore;//group2
            _modelFactory = modelFactory;
        }

        public ITextViewModel GetTextByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }

            return _textStore.GetOne(x => x.Name == name);//group2
            //return _temporaryDatabase.FirstOrDefault(x => x.GetName() == name);//gorup1
        }

        public bool AnyByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }
            return _textStore.Exists(x => x.Name == name);//group2
            //return _temporaryDatabase.Any(x => x.GetName() == name);//group1
        }

        public List<ITextViewModel> GetTextByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            return _textStore.GetMany(x => x.Author == author).Select(x => (ITextViewModel)x).ToList();//group2
            //return _temporaryDatabase.Where(x => x.GetAuthor() == author).Select(x => (ITextViewModel)x).ToList();//group1
        }

        public bool AnyByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            return _textStore.Exists(x => x.Author == author);//group2
            //return _temporaryDatabase.Any(x => x.GetAuthor() == author);//group1
        }

        public ITextViewModel CreateText(string name, TextReader input, int length, string author = null)
        {
            if (AnyByName(name))
            {
                throw new ArgumentException($"Cannot create text because another text in the database already has the name {name}.");
            }
            ITextModel model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            _textStore.Add(model);//group2
            //_temporaryDatabase.Add(model);//group1
            return model;
        }

        public void DeleteText(ITextViewModel model)
        {
            _textStore.Delete((ITextModel)model);//group2
            //_temporaryDatabase.Remove(model);//group1
        }

        public void UpdateText(ITextViewModel model, string name = null, string author = null, bool? includeQuotes = null)
        {
            var updatedModel = (ITextModel)model;
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedModel.SetName(name);
                _textStore.ModifyName((ITextModel)model, name);//group2
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                updatedModel.SetAuthor(author);
                _textStore.ModifyAuthor((ITextModel)model, author);//group2

            }
            if (includeQuotes != null)
            {
                updatedModel.SetIncludeQuotes((bool)includeQuotes);
                _textStore.ModifyIncludeQuotes((ITextModel)model, (bool)includeQuotes);//group2
            }
        }

        public List<ITextViewModel> GetAllTexts()
        {
            return _textStore.GetMany(x => true).Select(x => (ITextViewModel)x).ToList();//group2
            //return _temporaryDatabase.Where(x => true).Select(x => (ITextViewModel)x).ToList();//group1
        }
    }
}
